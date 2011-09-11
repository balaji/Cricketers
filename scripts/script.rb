require 'rubygems'
require 'open-uri'
require 'nokogiri'
(1..9).each do |i|
	('A'..'Z').each do |a|
		document = Nokogiri::HTML(open("http://www.espncricinfo.com/ci/content/player/country.html?country=#{i};alpha=#{a}"))
		document.xpath('//td[@class="ciPlayernames"]/a').each do |anchor|
			f.write "#{anchor.content},#{anchor['href']}"
		end
	end
end

