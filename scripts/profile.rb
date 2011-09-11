require 'rubygems'
require 'open-uri'
require 'nokogiri'
require 'net/http'
require 'json'

module Couch

  class Server
    def initialize(host, port, options = nil)
      @host = host
      @port = port
      @options = options
    end

    def delete(uri)
      request(Net::HTTP::Delete.new(uri))
    end

    def get(uri)
      request(Net::HTTP::Get.new(uri))
    end

    def put(uri, json)
      req = Net::HTTP::Put.new(uri)
      req["content-type"] = "application/json"
      req.body = json
      request(req)
    end

    def post(uri, json)
      req = Net::HTTP::Post.new(uri)
      req["content-type"] = "application/json"
      req.body = json
      request(req)
    end

    def request(req)
      res = Net::HTTP.start(@host, @port) { |http|http.request(req) }
      unless res.kind_of?(Net::HTTPSuccess)
        handle_error(req, res)
      end
      res
    end

    private

    def handle_error(req, res)
      e = RuntimeError.new("#{res.code}:#{res.message}\nMETHOD:#{req.method}\nURI:#{req.path}\n#{res.body}")
      raise e
    end
  end
end

server = Couch::Server.new("localhost", "5984")

{"India" => "india", "Pakistan" => "pakistan", "Sri Lanka" => "sl", "West Indies" => "wl", "Zimbabwe" => "zimbabwe"}.each do |k, v|
country = k
File.open("#{v}.txt", 'r') do |f|
	while(line = f.gets)
		bb = line.split("\t")
		url = bb[1]
		document = Nokogiri::HTML(open("http://www.espncricinfo.com#{url}"))

		static_profile = ["Full name", "Born", "Major teams", "Batting style", "Bowling style", "Fielding position"]
		static_debut = ["Test debut", "ODI debut", "T20I debut", "Twenty20 debut", "First-class debut"]
		static_averages = ["Tests", "ODIs", "T20Is", "First-class", "Twenty20"]
		internationals = ["Tests", "ODIs", "T20Is"]
		raw_averages = []
		data = {}
		debut = {}
		batting = {}
		bowling = {}
		profile = {}

		document.xpath('//p[@class="ciPlayerinformationtxt"]').each do |link|
			y = "" 
			link.xpath('span').each {|r| y += r.content }
			if static_profile.include?(link.at_xpath('b/text()').to_s)
				profile[link.at_xpath('b/text()').to_s.strip] = y.to_s.strip
			end
		end

		next if profile["Major teams"] == nil
		profile["Major teams"] = profile["Major teams"].split(',').each {|s| s.strip!}
	
		document.xpath('//table[@class="engineTable"]').each do |link|
			headers = []
			contents = []

			link.xpath('thead/tr/th').each do |h|
				headers.push(h.content.to_s.strip)
			end

			link.xpath('tbody/tr/td').each do |d|
				contents.push(d.content.to_s.strip)
			end
		
			if headers.length > 0
				(contents.length / headers.length).times do |j|
					stats = {}
					headers.length.times do |i|
						stats[headers[i]] = contents[i + (j * headers.length)]
					end	
					raw_averages.push(stats)	
				end
			end

			link.xpath('tr[@class="data2"]').each do |d|
				if static_debut.include?(d.at_xpath('td[1]/b/text()').to_s)
					debut[d.at_xpath('td[1]/b/text()').to_s.strip] = d.at_xpath('td[2]/text()').to_s.strip
				end 
			end	

		end

		key = ""
		raw_averages.each do |stat|
			if static_averages.include?(stat[key])
				value = stat[key]
				stat.delete(key)
				if(stat["100"])
					batting[value] = stat
				end

				if(stat["Wkts"])
					bowling[value] = stat
				end
			end
		end
	
		next if batting.empty? and bowling.empty?

		r1 = false
		r2 = false
		batting.keys.each {|z| r1 = internationals.include?(z); break if r1}
		bowling.keys.each {|z| r2 = internationals.include?(z); break if r2}

		next if !r1 and !r2

		img = document.at_xpath('//div[@class="pnl490M"]/div[2]/div[2]/img')
		image =  img ? img['src'].to_s : "" 

		data["bat"] = batting
		data["bowl"] = bowling
		data["profile"] = profile
		data["debut"] = debut
		data["image"] = image
		data["country"] = country

		unique_key = ''
		if data["profile"]["Born"] != nil and data["profile"]["Born"] != 'date unknown'
			unique_key = data["profile"]["Born"].split(',').join('_').split(' ').join('')
		end
		if data["profile"]["Major teams"].include?(country)
			server.put("/profile/#{data['profile']['Full name'].split(' ').join('_')}_#{unique_key}", data.to_json)
		end

	end
end
end

