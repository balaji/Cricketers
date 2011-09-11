require 'rubygems'
require 'couchrest'

server = CouchRest.new("http://localhost:5984") 
db = server.database('profile')
payload = {"map" => "function(doc)
{
var x = 'First-class';
	if((doc.bat['ODIs'] || doc.bowl['ODIs']) && ((doc.bowl[x] && (isNaN(doc.bowl[x]['Balls']) || doc.bowl[x]['Balls'].trim() == '')))) {
		emit(doc.bowl[x]['Balls'], null);
	}
}"}
rows = db.temp_view(payload)["rows"]
count = 0
rows.each do |d|
	flag = false
	doc = db.get(d["id"])
	stats = doc["bowl"]
	
	stats.each do |k, v|
		if(/\+$/.match(stats[k]["Balls"]))
			stats[k]["Balls"] = stats[k]["Balls"][0..stats[k]["Balls"].length - 2]	
			flag = true
		end
	end
	if flag
	#p stats
	db.save_doc(doc)
	count += 1
	end
end
p "saved #{count}"
