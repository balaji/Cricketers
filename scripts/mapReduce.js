//to figure out all profile that are does not contain correct balls count.
function(doc)
{
var x = "ODIs";
	if((doc.bat['ODIs'] || doc.bowl['ODIs']) && ((doc.bowl[x] && (isNaN(doc.bowl[x]['Balls']) || doc.bowl[x]['Balls'].trim() == '')))) {
		emit(doc.bowl[x]["Balls"], null);
	}
}

//to fetch records without debut info, that were missed in the first scrape
function(doc) {
	if(doc.bat['ODIs'] || doc.bowl['ODIs'])
	{
		if(/*(doc.bat['ODIs'] && doc.bat['ODIs']['Mat'] == "1" && doc["debut"]["ODI debut"] == null) ||
			(doc.bat['Tests'] && doc.bat['Tests']['Mat'] == "1" && doc["debut"]["Test debut"] == null) ||
			(doc.bat['T20Is'] && doc.bat['T20Is']['Mat'] == "1" && doc["debut"]["T20I debut"] == null) ||
			(doc.bat['Twenty20'] && doc.bat['Twenty20']['Mat'] == "1" && doc["debut"]["Twenty20 debut"] == null) ||*/
			(doc.bat['First-class'] && doc.bat['First-class']['Mat'] == "1" && doc["debut"]["First-class debut"] == null)) 
		{
			emit(doc.profile["Full name"], null);
		}
	}
}

