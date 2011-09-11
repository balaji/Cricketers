require 'rubygems'
require 'couchrest'
require 'nokogiri'
require 'open-uri'

File.open("single-match-players.txt", 'r') do |f|

	static_debut =  ["Only Test", "Only ODI", "Only T20I", "Only Twenty20", "Only First-class"]
	static_profile = ["Full name", "Born", "Major teams", "Batting style", "Bowling style", "Fielding position"]

	server = CouchRest.new("http://localhost:5984") 
	db = server.database('profile')
	count = 0	
	while(line = f.gets)
		debut = {}
		profile = {}
		bb = line.split("\t")
		url = bb[1]
		document = Nokogiri::HTML(open("http://www.espncricinfo.com#{url}"))
		document.xpath('//p[@class="ciPlayerinformationtxt"]').each do |link|
			y = "" 
			link.xpath('span').each {|r| y += r.content }
			if static_profile.include?(link.at_xpath('b/text()').to_s)
				profile[link.at_xpath('b/text()').to_s.strip] = y.to_s.strip
			end
		end

		profile["Major teams"] = profile["Major teams"].split(',').each {|s| s.strip!}
		
		document.xpath('//table[@class="engineTable"]').each do |link|
			link.xpath('tr[@class="data2"]').each do |d|

				if static_debut.include?(d.at_xpath('td[1]/b/text()').to_s)
					p "in"									
					debut[d.at_xpath('td[1]/b/text()').to_s.strip] = d.at_xpath('td[2]/text()').to_s.strip
				end 
			end	
		end
		unique_key = ''
		if profile["Born"] != nil and profile["Born"] != 'date unknown'
			unique_key = profile["Born"].split(',').join('_').split(' ').join('')
		end
		p debut
		if (!debut.empty?)
			id = "#{profile['Full name'].split(' ').join('_')}_#{unique_key}"
			doc = db.get(id)
			debut.each do |k,v|
				doc["debut"][k] = v
			end
			db.save_doc(doc)
			count += 1
			p id
		end
		#break
	end
	p "saved #{count}"
end
