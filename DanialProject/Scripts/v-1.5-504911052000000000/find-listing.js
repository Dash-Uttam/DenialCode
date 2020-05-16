var buildings = [];
var origin = { lat: 40.6927172, lng: -73.924730299999965 };
var destination = { lat: 40.6927172, lng: -73.924730299999965 };
(function () {

})();

function gotoSingleListing(id) {
    window.location = '../../Home/SingleListing/' + id;
}
function AgentInfo(id) {
    location.href = '../../Home/SingleAgent/' + id;
}

function initMap() {
    $("#preloader").show();
    // Styles a map in night mode.
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 40.674, lng: -73.945 },
        zoom: 12,
        mapTypeControl: false,
        fullscreenControl: false,
        streetViewControl: false,
        styles: [
            {
                "stylers": [
                    {
                        "visibility": "simplified"
                    }
                ]
            },
            {
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#523735"
                    }
                ]
            },
            {
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#f5f1e6"
                    }
                ]
            },
            {
                "featureType": "administrative",
                "stylers": [
                    {
                        "color": "#293847"
                    },
                    {
                        "visibility": "simplified"
                    }
                ]
            },
            {
                "featureType": "administrative.land_parcel",
                "elementType": "labels",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "administrative.locality",
                "stylers": [
                    {
                        "visibility": "simplified"
                    }
                ]
            },
            {
                "featureType": "administrative.locality",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "visibility": "on"
                    }
                ]
            },
            {
                "featureType": "landscape",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#eaeaea"
                    },
                    {
                        "visibility": "simplified"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#c3ecb2"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#293847"
                    }
                ]
            },
            {
                "featureType": "road",
                "stylers": [
                    {
                        "lightness": -5
                    },
                    {
                        "visibility": "simplified"
                    },
                    {
                        "color": "#e0e0e0"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "stylers": [
                    {
                        "color": "#f09b70"
                    },
                    {
                        "visibility": "simplified"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "labels",
                "stylers": [
                    {
                        "color": "#523735"
                    }
                ]
            },
            {
                "featureType": "road.local",
                "elementType": "labels",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "transit.station.airport",
                "stylers": [
                    {
                        "visibility": "on"
                    }
                ]
            },
            {
                "featureType": "water",
                "stylers": [
                    {
                        "color": "#aab9c7"
                    }
                ]
            }
        ]
    });

    //40.6652° N, 73.9125° W


    var transitLayer = new google.maps.TransitLayer();
    transitLayer.setMap(map);

    //40.6652° N, 73.9125° W

    var Previous;
    var P_type;
    //get all building markers
    $.getJSON("/api/webapis/getallbuildings", function (data) {
        var markers = [];
        $.each(data, function (index, item) {
            buildings.push(item);

            var image = "../Content/Template/assets/imgs/Icons/map-pin-mint.png";
            if (item.Type === "Rent") {
                image = "../Content/Template/assets/imgs/Icons/map-pin-mint.png";
            }
            else {
                image = "../Content/Template/assets/imgs/Icons/map-pin-coral.png";
            }

            // Add some markers to the map.
            // Note: The code uses the JavaScript Array.prototype.map() method to
            // create an array of markers based on a given "locations" array.
            // The map() method here has nothing to do with the Google Maps API.  
            var marker = new google.maps.Marker({
                zoom: 8,
                position: { lat: item.Latitude, lng: item.Longitude },
                //map: map,
                title: '' + item.Address,
                icon: {
                    url: image,
                    scaledSize: new google.maps.Size(18, 32)
                },
                id: item.ID
            });

            //Add Listener
            google.maps.event.addListener(marker, 'click', function () {
                CopyLink(marker.id);
                $("#preloader").show();
                var title = marker.id;
                var building = null;
                for (var i = 0; i < buildings.length; i++) {
                    if (buildings[i].ID === title) {
                        building = buildings[i];
                        break;
                    }
                }

                if (P_type === "Rent" && P_type !== "") {
                    var R_icon = {
                        url: '../Content/Template/assets/imgs/Icons/map-pin-mint.png',
                        scaledSize: new google.maps.Size(18, 32)
                    };
                    Previous.setIcon(R_icon);
                }
                else if (P_type === "Sale" && P_type !== "") {
                    var S_icon = {
                        url: '../Content/Template/assets/imgs/Icons/map-pin-coral.png',
                        scaledSize: new google.maps.Size(18, 32)
                    };
                    Previous.setIcon(S_icon);
                }

                if (building.Type === "Rent") {
                    R_icon = {
                        url: '../Content/Template/assets/imgs/Icons/map-pin-mint.png',
                        scaledSize: new google.maps.Size(28, 50)
                    };
                    marker.setIcon(R_icon);

                    Previous = marker;
                    P_type = "Rent";
                }
                else {
                    S_icon = {
                        url: '../Content/Template/assets/imgs/Icons/map-pin-coral.png',
                        scaledSize: new google.maps.Size(28, 50)
                    };
                    marker.setIcon(S_icon);

                    Previous = marker;
                    P_type = "Sale";
                }

                var counter = 1;
                $('#carouselExample').html('');
                $.ajax({
                    type: "GET",
                    url: "/api/WebApis/GetGalleryImages?BuildingId=" + marker.id + "", //URI
                    dataType: "json",
                    success: function (data) {
                        var datavalue = data;
                        var myJsonObject = datavalue;
                        contentType: "application/json";
                        $.each(myJsonObject.list, function (i, mobj) {
                            if (counter === 1) {
                                if (data.Type === "Rent" && data.No_Fee === true) {
                                    var html = '<div class="carousel-item active img-box" style="height:200px; position: relative;">' +
                                        '<img src= "' + mobj.Path + '" onclick= "gotoSingleListing(' + mobj.BuildingsID + ');" style="cursor:pointer; height:100%; width:auto;" class="d-block center" alt= "..." >' +
                                        '<img src="../../Content/Template/assets/imgs/Icons/nofree.png" class="nofee-img" />' +
                                        '</div>';
                                    $('#carouselExample').append(html);
                                    counter++;
                                }
                                else {
                                    var html1 = '<div class="carousel-item img-box active" style="height:200px;">' +
                                        '<img src= "' + mobj.Path + '" onclick= "gotoSingleListing(' + mobj.BuildingsID + ');" style="cursor:pointer; height:100%; width:auto;" class="d-block center" alt= "..." >' +
                                        '</div>';
                                    $('#carouselExample').append(html1);
                                    counter++;
                                }
                            }
                            else {
                                if (data.Type === "Rent" && data.No_Fee === true) {
                                    var html2 = '<div class="carousel-item img-box" style="height:200px; position: relative;">' +
                                        '<img src= "' + mobj.Path + '" onclick= "gotoSingleListing(' + mobj.BuildingsID + ');" style="cursor:pointer; height:100%; width:auto;" class="d-block center" alt= "..." >' +
                                        '<img src="../../Content/Template/assets/imgs/Icons/nofree.png" class="nofee-img" />' +
                                        '</div>';
                                    $('#carouselExample').append(html2);
                                    counter++;
                                }
                                else {
                                    var html3 = '<div class="carousel-item img-box" style="height:200px;">' +
                                        '<img src= "' + mobj.Path + '" onclick= "gotoSingleListing(' + mobj.BuildingsID + ');" style="cursor:pointer; height:100%; width:auto;" class="d-block center" alt= "..." >' +
                                        '</div>';
                                    $('#carouselExample').append(html3);
                                }
                            }
                        });
                    },
                    error: function (err, textStatus, errorThrown) {
                        $("#preloader").hide();
                        var dialog = bootbox.dialog({
                            message: '<p class="text-center">There was an error retrieving your requested result. Please try again.</p>',
                            closeButton: false
                        });
                        setTimeout(function () { }, 2000);
                    }
                });

                origin = { lat: building.Latitude, lng: building.Longitude };
                var sp = building.Address.split(",");
                var name = sp[0];
                var address;
                if (sp[0] === building.Name) {
                    address = "";
                }
                else {
                    address = building.Name;
                }
                $('#buildingAddress').text(address);
                $('#buildingRent').text(dotDivider(building.Price));
                $('#buildingRooms').text(building.Beds + ' Bed / ' + building.Baths + ' Baths');
                $('#ChangeBorough').text(building.Borough);
                $('#ChangeNeighborhood').text(building.Neighbourhood);
                document.getElementById("unit").innerHTML = building.Unit;

                $('#agent-media').html('');
                if (building.Type === "Rent") {
                    $.ajax({
                        type: "GET",
                        url: "/api/WebApis/GetListingAsync?Id=" + marker.id + "", //URI
                        dataType: "json",
                        success: function (data) {
                            var datavalue = data;
                            var myJsonObject = datavalue;
                            contentType: "application/json";
                            try {
                                document.getElementById("label1").innerHTML = "OP";
                                document.getElementById("label2").innerHTML = "FEE";
                                document.getElementById("label3").innerHTML = "LEASE TERM";
                                document.getElementById("label4").innerHTML = "AVAILABILITY";
                                document.getElementById("label5").innerHTML = "WEEKS FREE";
                                document.getElementById("label6").innerHTML = "EXCLUSIVITY";
                                document.getElementById("label7").innerHTML = "BLD. ACCS.";
                                document.getElementById("label8").innerHTML = "UNIT ACCS.";

                                document.getElementById("data1").innerHTML = data.OtherDetails.OP;
                                document.getElementById("data2").innerHTML = data.OtherDetails.Fee;
                                document.getElementById("data3").innerHTML = data.OtherDetails.Lease_term;
                                var date = new Date(data.OtherDetails.AvalibilityDate);
                                document.getElementById("data4").innerHTML = date.toLocaleDateString();
                                document.getElementById("data5").innerHTML = data.OtherDetails.Incentives;
                                document.getElementById("data6").innerHTML = data.OtherDetails.Exclusivity;
                                document.getElementById("data7").innerHTML = data.OtherDetails.Building_Access;
                                document.getElementById("data8").innerHTML = data.OtherDetails.Unit_Access;
                                document.getElementById("access-notes").innerHTML = data.OtherDetails.Access_Notes;
                            } catch (Exception) { /*Comment*/ }

                            try {
                                if (data.OtherDetails.Exclusivity !== "Exclusive") {
                                    var n1 = name.split(" ");
                                    var address_for_users = "";
                                    for (var y = 1; y < n1.length; y++) {
                                        address_for_users = address_for_users + n1[y] + " ";
                                    }
                                    $('#buildingName').html('<a id="listingLink" class="user-link" onclick="gotoSingleListing(' + marker.id + ')"><h2 class="font1" style="font-size:38px !important;">' + address_for_users + '</h2></a>');
                                    $('#buildingName-Agent').html('<a id="listingLink" class="user-link" onclick="gotoSingleListing(' + marker.id + ')"><h2 class="font1" style="font-size:38px !important;">' + name + '</h2></a>');
                                }
                                else {
                                    $('#buildingName').html('<a id="listingLink" class="user-link" onclick="gotoSingleListing(' + marker.id + ')"><h2 class="font1" style="font-size:38px !important;">' + name + '</h2></a>');
                                    $('#buildingName-Agent').html('<a id="listingLink" class="user-link" onclick="gotoSingleListing(' + marker.id + ')"><h2 class="font1" style="font-size:38px !important;">' + name + '</h2></a>');
                                }
                            } catch (error) { /*Comment*/ }

                            if (data.OtherDetails.SalesAgents.length < 1) {
                                var html = `<div class="media">
                                 <div class="m-r-10 polygon polygon-wrap-small cursor-pointer img-box-agent" onclick="AgentInfo(${data.OtherDetails.Agents.ID});">
                                    <img src="${data.OtherDetails.Agents.Path}" class="display-box" />
                                </div>
                                <div class="media-body listing-media-body">
                                    <a href="../../Home/SingleAgent/${ data.OtherDetails.Agents.ID}" class="user-link"><h5 class="mt-1 font1"><span class="agent-title-smal d-block"><small>${data.OtherDetails.Agents.Position}</small></span> ${data.OtherDetails.Agents.FirstName} ${data.OtherDetails.Agents.LastName}</h5></a>
                                    <a href="tel:${ data.OtherDetails.Agents.Phone}"><u>${data.OtherDetails.Agents.Phone}</u></a><br />
                                    <a href="mailto:${ data.OtherDetails.Agents.EmailField}"><u>${data.OtherDetails.Agents.EmailField}</u></a>
                                </div>
                            </div>`;

                                $('#agent-media').html(html);
                            }
                            else {
                                $('#agent-media').html('');
                                for (var i = 0; i < data.OtherDetails.SalesAgents.length; i++) {
                                    var split = data.OtherDetails.SalesAgents[i].split("_");
                                    $.ajax({
                                        type: "GET",
                                        url: "/api/WebApis/GetAgentsInfo?Id=" + split[1] + "", //URI
                                        dataType: "json",
                                        success: function (data) {
                                            var datavalue = data;
                                            var myJsonObject = datavalue;
                                            contentType: "application/json";
                                            try {
                                                var html = `<div class="media">
                                                        <div class="m-r-10 polygon polygon-wrap-small cursor-pointer img-box-agent" onclick="AgentInfo(${data.ID});">
                                                            <img src="${data.Path}" class="display-box" />
                                                        </div>
                                                        <div class="media-body listing-media-body">
                                                            <a href="../../Home/SingleAgent/${ data.ID}" class="user-link"><h5 class="mt-1 font1"><span class="agent-title-smal d-block"><small>${data.Position}</small></span> ${data.FirstName} ${data.LastName}</h5></a>
                                                            <a href="tel:${ data.Phone}"><u>${data.Phone}</u></a><br />
                                                            <a href="mailto:${ data.EmailField}"><u>${data.EmailField}</u></a>
                                                        </div>
                                                    </div>
                                                <div>
                                                    <hr />
                                                </div>`;
                                                $('#agent-media').append(html);
                                            } catch (Exception) { /*Comment*/ }
                                        },
                                        error: function (err, textStatus, errorThrown) {
                                            $("#preloader").hide();
                                        }
                                    });
                                }
                            }
                            $("#preloader").hide();
                        },
                        error: function (err, textStatus, errorThrown) {
                            $("#preloader").hide();
                        }
                    });
                }
                else {
                    $('#buildingName-Agent').html('<a id="listingLink" class="user-link" onclick="gotoSingleListing(' + marker.id + ')"><h2 class="font1" style="font-size:38px !important;">' + name + '</h2></a>');
                    $('#buildingName').html('<a id="listingLink" class="user-link" onclick="gotoSingleListing(' + marker.id + ')"><h2 class="font1" style="font-size:38px !important;">' + name + '</h2></a>');
                    $.ajax({
                        type: "GET",
                        url: "/api/WebApis/GetListingAsync?Id=" + marker.id + "", //URI
                        dataType: "json",
                        success: function (data) {
                            var datavalue = data;
                            var myJsonObject = datavalue;
                            contentType: "application/json";
                            try {
                                //labels
                                document.getElementById("label1").innerHTML = "BLD. TYPE";
                                document.getElementById("label2").innerHTML = "COMMISSION";
                                document.getElementById("label3").innerHTML = "TAX";
                                document.getElementById("label4").innerHTML = "SQ FT";
                                document.getElementById("label5").innerHTML = "MAINT. / CC";
                                document.getElementById("label6").innerHTML = "FINANCING";
                                document.getElementById("label7").innerHTML = "BLD. ACCS.";
                                document.getElementById("label8").innerHTML = "UNIT ACCS.";

                                //Data
                                document.getElementById("data1").innerHTML = data.BuildingType;
                                document.getElementById("data2").innerHTML = data.SaleBuilding.Commision;
                                document.getElementById("data3").innerHTML = data.SaleBuilding.Tax;
                                document.getElementById("data4").innerHTML = data.SaleBuilding.SqFt;
                                document.getElementById("data5").innerHTML = data.SaleBuilding.Maintenance;
                                document.getElementById("data6").innerHTML = data.SaleBuilding.MaxFinancing;
                                document.getElementById("data7").innerHTML = data.SaleBuilding.Building_Access;
                                document.getElementById("data8").innerHTML = data.SaleBuilding.Unit_Access;
                                document.getElementById("access-notes").innerHTML = data.SaleBuilding.Access_Notes;
                            } catch (Exception) { /*Comment*/ }

                            var html = `<div class="media">
                                <div class="m-r-10 polygon polygon-wrap-small cursor-pointer img-box-agent" onclick="AgentInfo(${data.Agents.ID});">
                                    <img src="${data.Agents.Path}" class="display-box" />
                                </div>
                                <div class="media-body listing-media-body">
                                    <a href="../../Home/SingleAgent/${ data.Agents.ID}" class="user-link"><h5 class="mt-1 font1"><span class="agent-title-smal d-block"><small>${data.Agents.Position}</small></span> ${data.Agents.FirstName} ${data.Agents.LastName}</h5></a>
                                    <a href="tel:${ data.Agents.Phone}"><u>${data.Agents.Phone}</u></a><br />
                                    <a href="mailto:${ data.Agents.EmailField}"><u>${data.Agents.EmailField}</u></a>
                                </div>
                              </div>`;
                            $('#agent-media').html(html);
                            $("#preloader").hide();
                        },
                        error: function (err, textStatus, errorThrown) {
                            $("#preloader").hide();
                        }
                    });
                }

                $('#detailsModal').addClass('active');
                $('#filter-card').css({
                    left: '-700px'
                });
                $('a.filter-left-collapse').hide();
                $('a.filter-right-open').addClass('active');
                $('#map').addClass('map-width');
                $('#filter-maps').addClass('filter-list-map-after');
            });
            //Add Listener

            markers.push(marker);

            //marker.setMap(map);
        });

        // Add a marker clusterer to manage the markers.
        var markerCluster = new MarkerClusterer(map, markers,
            {
                maxZoom: 12,
                //imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m'
                styles: [{  
                    url: "../../Content/Template/assets/imgs/maps/1.png",
                    width: 53,
                    height: 52,   
                    textSize: 16,
                    textColor: "white" 
                }]
            });
        setTimeout(function () {
            $("#preloader").hide();
        }, 800);
    });

    ///////////////////////////////
    //////////////////////////////
    ////
    //Calculate Distance Api 
    var input = document.getElementById('Address');
    var autocomplete = new google.maps.places.Autocomplete(input);

    autocomplete.setFields(
        ['address_components', 'geometry', 'icon', 'name']);

    autocomplete.addListener('place_changed', function () {
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            // User entered the name of a Place that was not suggested and
            // pressed the Enter key, or the Place Details request failed.
            window.alert("No details available for input: '" + place.name + "'");
            return;
        }
        var address = '';
        if (place.address_components) {
            address = [
                (place.address_components[0] && place.address_components[0].short_name || ''),
                (place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[2] && place.address_components[2].short_name || ''),
                (place.address_components[3] && place.address_components[3].short_name || ''),
                (place.address_components[4] && place.address_components[4].short_name || ''),
                (place.address_components[5] && place.address_components[5].short_name || ''),
                (place.address_components[6] && place.address_components[6].short_name || ''),
                (place.address_components[7] && place.address_components[7].short_name || '')

            ].join(' ');
        }
        destination = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
    });
}

function CalculateDistance() {
    var destinationIcon = 'https://chart.googleapis.com/chart?' +
        'chst=d_map_pin_letter&chld=D|FF0000|000000';
    var originIcon = 'https://chart.googleapis.com/chart?' +
        'chst=d_map_pin_letter&chld=O|FFFF00|000000';
    var geocoder = new google.maps.Geocoder;

    var service = new google.maps.DistanceMatrixService;
    service.getDistanceMatrix({
        origins: [origin],
        destinations: [destination],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.IMPERIAL,
        avoidHighways: false,
        avoidTolls: false
    }, function (response, status) {
        if (status !== 'OK') {
            document.getElementById("").innerHTML = "We are sorry we are unable to calculate distance for you.";
        } else {
            var originList = response.originAddresses;
            var destinationList = response.destinationAddresses;
            var outputDiv = document.getElementById('Distance-Info');
            outputDiv.innerHTML = '';

            var showGeocodedAddressOnMap = function (asDestination) {
                var icon = asDestination ? destinationIcon : originIcon;
                return function (results, status) {
                };
            };

            try {
                for (var i = 0; i < originList.length; i++) {
                    var results = response.rows[i].elements;
                    geocoder.geocode({ 'address': originList[i] },
                        showGeocodedAddressOnMap(false));
                    for (var j = 0; j < results.length; j++) {
                        geocoder.geocode({ 'address': destinationList[j] },
                            showGeocodedAddressOnMap(true));
                        outputDiv.innerHTML += destinationList[j] +
                            ': ' + results[j].distance.text + ' / ' +
                            results[j].duration.text + '<br>';
                    }
                }
            } catch (error) { outputDiv.innerHTML = "We are sorry we are unable to calculate distance for you."; }
        }
    });

    document.getElementById("Ditance-Search-Field").style.display = "none";
    document.getElementById("Ditance-Info-Field").style.display = "initial";
    return false;
}
function CalcDistance() {
    document.getElementById("Address").value = "";
    document.getElementById("Ditance-Search-Field").style.display = "flex";
    document.getElementById("Ditance-Info-Field").style.display = "none";
}

$('.map-listing-close').click(function () {
    $('#map').removeClass('map-width');
    $('#filter-maps').removeClass('filter-list-map-after');
    $('#detailsModal').removeClass('active');
});

$('.filter-left-collapse').click(function () {
    $('#filter-card').css({
        left: '-700px'
    });
    $(this).hide();
    $('a.filter-right-open').addClass('active');
});
$('.filter-right-open').click(function () {
    $(this).removeClass('active');
    $('#filter-card').css({
        left: '40px'
    });
    $('a.filter-left-collapse').show();
    $('#map').removeClass('map-width');
    $('#filter-maps').removeClass('filter-list-map-after');
    $('#detailsModal').removeClass('active');
});

function BroughChange() {
    var a = document.getElementById("Brough").selectedIndex;
    var b = document.getElementById("Brough").options;

    if (b[a].value === "Brooklyn") {
        document.getElementById("Brooklyn").style.display = "initial";
        document.getElementById("Manhattan").style.display = "none";
        document.getElementById("Queens").style.display = "none";
        document.getElementById("TheBronx").style.display = "none";
        document.getElementById("StatenIsland").style.display = "none";
        document.getElementById("LongIsland").style.display = "none";
        document.getElementById("UpstateNY").style.display = "none";
        document.getElementById("EmptySelect").style.display = "none";
        BrooklynChange();
    }
    else if (b[a].value === "Manhattan") {
        document.getElementById("Brooklyn").style.display = "none";
        document.getElementById("Manhattan").style.display = "initial";
        document.getElementById("Queens").style.display = "none";
        document.getElementById("TheBronx").style.display = "none";
        document.getElementById("StatenIsland").style.display = "none";
        document.getElementById("LongIsland").style.display = "none";
        document.getElementById("UpstateNY").style.display = "none";
        document.getElementById("EmptySelect").style.display = "none";
        ManhattanChange();
    }
    else if (b[a].value === "Queens") {
        document.getElementById("Brooklyn").style.display = "none";
        document.getElementById("Manhattan").style.display = "none";
        document.getElementById("Queens").style.display = "initial";
        document.getElementById("TheBronx").style.display = "none";
        document.getElementById("StatenIsland").style.display = "none";
        document.getElementById("LongIsland").style.display = "none";
        document.getElementById("UpstateNY").style.display = "none";
        document.getElementById("EmptySelect").style.display = "none";
        QueensChange();
    }
    else if (b[a].value === "The Bronx") {
        document.getElementById("Brooklyn").style.display = "none";
        document.getElementById("Manhattan").style.display = "none";
        document.getElementById("Queens").style.display = "none";
        document.getElementById("TheBronx").style.display = "initial";
        document.getElementById("StatenIsland").style.display = "none";
        document.getElementById("LongIsland").style.display = "none";
        document.getElementById("UpstateNY").style.display = "none";
        document.getElementById("EmptySelect").style.display = "none";
        TheBronxChange();
    }
    else if (b[a].value === "Staten Island") {
        document.getElementById("Brooklyn").style.display = "none";
        document.getElementById("Manhattan").style.display = "none";
        document.getElementById("Queens").style.display = "none";
        document.getElementById("TheBronx").style.display = "none";
        document.getElementById("StatenIsland").style.display = "initial";
        document.getElementById("LongIsland").style.display = "none";
        document.getElementById("UpstateNY").style.display = "none";
        document.getElementById("EmptySelect").style.display = "none";
        StatenIslandChange();
    }
    else if (b[a].value === "Long Island") {
        document.getElementById("Brooklyn").style.display = "none";
        document.getElementById("Manhattan").style.display = "none";
        document.getElementById("Queens").style.display = "none";
        document.getElementById("TheBronx").style.display = "none";
        document.getElementById("StatenIsland").style.display = "none";
        document.getElementById("LongIsland").style.display = "initial";
        document.getElementById("UpstateNY").style.display = "none";
        document.getElementById("EmptySelect").style.display = "none";
        LongIslandChange();
    }
    else {
        document.getElementById("Brooklyn").style.display = "none";
        document.getElementById("Manhattan").style.display = "none";
        document.getElementById("Queens").style.display = "none";
        document.getElementById("TheBronx").style.display = "none";
        document.getElementById("StatenIsland").style.display = "none";
        document.getElementById("LongIsland").style.display = "none";
        document.getElementById("UpstateNY").style.display = "initial";
        document.getElementById("EmptySelect").style.display = "none";
        UpstateNYChange();
    }
}

function BrooklynChange() {
    var a = document.getElementById("Brooklyn").selectedIndex;
    var b = document.getElementById("Brooklyn").options;

    document.getElementById("ChangeNeighborhood").innerHTML = b[a].value;
}
function ManhattanChange() {
    var a = document.getElementById("Manhattan").selectedIndex;
    var b = document.getElementById("Manhattan").options;

    document.getElementById("ChangeNeighborhood").innerHTML = b[a].value;
}
function QueensChange() {
    var a = document.getElementById("Queens").selectedIndex;
    var b = document.getElementById("Queens").options;

    document.getElementById("ChangeNeighborhood").innerHTML = b[a].value;
}
function TheBronxChange() {
    var a = document.getElementById("TheBronx").selectedIndex;
    var b = document.getElementById("TheBronx").options;

    document.getElementById("ChangeNeighborhood").innerHTML = b[a].value;
}
function StatenIslandChange() {
    var a = document.getElementById("StatenIsland").selectedIndex;
    var b = document.getElementById("StatenIsland").options;

    document.getElementById("ChangeNeighborhood").innerHTML = b[a].value;
}
function LongIslandChange() {
    //var a = document.getElementById("LongIsland").selectedIndex;
    //var b = document.getElementById("LongIsland").options;

    document.getElementById("ChangeNeighborhood").innerHTML = "";
}
function UpstateNYChange() {
    //var a = document.getElementById("UpstateNY").selectedIndex;
    //var b = document.getElementById("UpstateNY").options;

    document.getElementById("ChangeNeighborhood").innerHTML = "";
}


function ChangeDisability(id) {
    if (id === "RESIDENTIAL") {
        $('#RESIDENTIAL').removeClass('disablilty');
        $('#COMMERCIAL').addClass('disablilty');
    }
    else {
        $('#COMMERCIAL').removeClass('disablilty');
        $('#RESIDENTIAL').addClass('disablilty');
    }
}

function PetsCheck() {
    var count = 0;
    $("input[name='pets-allowed']").each(function () {
        if ($(this).is(":checked")) {
            count++;
        }
    });
    if (count === 0) {
        $('#under30lbs').removeClass('enable');
        $('#casebycase').removeClass('enable');
        $("#under30lbs").addClass("disable");
        $("#casebycase").addClass("disable");
    }
    else {
        $('#under30lbs').removeClass('disable');
        $('#casebycase').removeClass('disable');
        $("#under30lbs").addClass("enable");
        $("#casebycase").addClass("enable");
    }
}
function ParkingCheck() {
    var count = 0;
    $("input[name='parking']").each(function () {
        if ($(this).is(":checked")) {
            count++;
        }
    });
    if (count === 0) {
        $('.AdditionalCost').removeClass('enable');
        $('.AdditionalCost').addClass("disable");
    }
    else {
        $('.AdditionalCost').removeClass('disable');
        $('.AdditionalCost').addClass("enable");
    }
}
