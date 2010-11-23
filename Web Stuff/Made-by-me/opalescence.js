if(Ultradent && $Ultradent) {
	Opalescence = function() { }
	
	Opalescence.prototype = {
		_numberOfPages: 0,
		_latitude: 0,
		_longitude: 0,
		_address: "",
		_name: "",
		_timeOut: null,
		
		/**
		 * populateDentistListUsingPagination creates a list of dentists based on a zip code, search radius, and product code from the form.
		 * @param {string} zipCode Zip code entered into form.
		 * @param {int} radius Search radius relative to zip code.
		 * @param {string} productCode The product code from the form.
		 * @param {int} limit The number of entries to display per page and paginate
		 * @param {string} debug Whether or not this is in debug mode
		 */
		populateDentistListUsingPagination: function(zipCode, radius, productCode, limit, debug) {
			$Ultradent.postJson("/business/GeographyService.php?getLatitudeAndLogitudeForZip", "{ \"zipCode\": \"" + zipCode + "\" }", function() {
				$("<img id=\"searching\" class=\"loadingImage\" src=\"/images/SearchingForDentists_257x55.gif\" alt=\"Searching for dentists\" />").appendTo("#resultsPlaceHolder");
				
				_timeOut = setTimeout('$("<h4>Please be patient. This search will take a few more seconds...</h4>").appendTo("#resultsPlaceHolder")', 15000);
			}, function(json) {
				$Ultradent.setJson(json);
				
				this.localJson = json;
				
				if(this.localJson.latitude != null && this.localJson.longitude != null) {
					_latitude = this.localJson.latitude;
					_longitude = this.localJson.longitude;
					
					$Ultradent.postJson("/business/GeographyService.php?getZipCodesInRadius", "{ \"radius\": " + radius + ", \"latitude\": \"" + this.localJson.latitude + "\", \"longitude\": \"" + this.localJson.longitude + "\", \"zipCode\": \"" + zipCode + "\" }", null, function(json) {
						$Ultradent.setJson(json);
						
						this.localJson = json;

						if(this.localJson.zipCodes) {
							$Ultradent.postJson("/business/DentistSelectionService.php?listDentists", "{ \"zipCodes\": " + JSON.stringify(this.localJson.zipCodes) + ", \"product\": \"" + productCode + "\" }", null, function(json) {
								$Ultradent.setJson(json);
								
								this.localJson = json;
								
								if(this.localJson.dentists && this.localJson.dentists.length > 0) {
									$("#resultsPlaceHolder").remove();
									clearTimeout(_timeOut);
									
									var controlBox = "<div id=\"controlBox\"></div>";
									
									$(controlBox).appendTo("#resultContainer");
									
									var paginationControl = "<div id=\"paginationControl\" class=\"paginationControl\"></div>";
									
									$(paginationControl).appendTo("#controlBox");

									var sortControl = "<div class=\"sortByBox\"><label for=\"sortBy\">Sort By:</label><select id=\"sortBy\" name=\"sortBy\"><option value=\"distance\">Distance</option></select></div>";
									
									$(sortControl).appendTo("#controlBox");
									
									var dentistListingsHeaders = "";
									if($("#products").val() == "any") {
										dentistListingsHeaders = "<div class=\"dentistListings\">" +
										"<div class=\"dentist-info header\">Dentist</div>" +
										"<div class=\"contact-info header\">Contact</div>" +
										"<div class=\"products header\">Products</div>" +
										"<div class=\"distance header\">Dist.</div>" +
										"<div class=\"directions header\">Directions</div>" +
										"</div>";
									} else {
										dentistListingsHeaders = "<div class=\"dentistListings\">" +
											"<div class=\"dentist header\">Dentist</div>" +
											"<div class=\"contact header\">Contact</div>" +
											"<div class=\"address header\">Address</div>" +
											"<div class=\"distance header\">Dist.</div>" +
											"<div class=\"directions header\">Directions</div>" +
											"</div>";
									}
								
									$(dentistListingsHeaders).appendTo("#resultContainer");
									
									var dentistPages = "<div id=\"dentistPages\" class=\"dentistPages\"></div>";
								
									$(dentistPages).appendTo("#resultContainer");
									
									this.dentists = this.localJson.dentists;
									
									dentistCounter = 0;
									pageCounter = 0;
									for(dentistIndex = 0, max = this.dentists.length, dentistCounter = 0, pageNumber = 1, nextPage = null; dentistIndex < max; dentistIndex++) {
										var nextDentist = this.dentists[dentistIndex];
										
										if(nextDentist) {
											if((dentistCounter % limit) == 0) {
												if(nextPage) {
													nextPage += "</div>";
													
													$(nextPage).appendTo("#dentistPages");
													
													nextPage = null;
													
													$Ultradent.Opalescence.setNumberOfPages(pageNumber);
												}
												
												nextPage = "<div id=\"page" + pageNumber + "\" class=\"page" + ((dentistCounter > 0) ? " hidden" : " visible") + "\">";														
												
												pageNumber++;
												pageCounter++;															
											}
											
											if("".capitalize) {
												var nameParts = (nextDentist.NAME.toLowerCase()).split(" ");
												
												for(index = 0; index < nameParts.length; index++) {
													nameParts[index] = nameParts[index].capitalize();
												}
												
												_name = nameParts.join(" ");
											} else {
												_name = nextDentist.NAME;
											}
											
											var dentistListing = "<div class=\"full dentistListing" + (dentistCounter % 2 ? "" : " odd") + "\">";
											
											if($("#products").val() == "any") {
												dentistListing += "<div class=\"dentist-info\">" +
												"<h6>" + nextDentist.NAME.toLowerCase() + "</h6>";
										
												if(nextDentist.ADDRESS1 && nextDentist.CITY && nextDentist.STATE && nextDentist.ZIPCODE) {
													dentistListing += "<div>" + nextDentist.ADDRESS1.toLowerCase() + "</div>";
												
													if(nextDentist.ADDRESS2) {
														var address2IsValid = /^(STE|SUITE|ste|Ste|suite|Suite){1}/.test(nextDentist.ADDRESS2);
														
														if(address2IsValid) {
															dentistListing += "<div>" + nextDentist.ADDRESS2.toLowerCase() + "</div>";
														}
													}
													
													dentistListing += "<div>" + nextDentist.CITY.toLowerCase() + ", " + nextDentist.STATE + " " + nextDentist.ZIPCODE + "</div>";
												}

												dentistListing += "</div>";
												
												var formattedPhone = nextDentist.PHONE.substring(0, 3) + "-" + nextDentist.PHONE.substring(4, 7) + "-" + nextDentist.PHONE.substring(7, 11);														
												dentistListing += "<div class=\"contact-info\"><div>" + formattedPhone + "</div>";
												
												if(nextDentist.EMAIL) {
													dentistListing += "<div>" +
														"<a href=\"mailto:" + nextDentist.EMAIL.toLowerCase() + "\">" + nextDentist.EMAIL.toLowerCase() + "</a>" + 
														"</div>";
												}
												
												dentistListing += "</div>";
												
												var products = "<div class=\"products\"><ul>";
												var purchased = new Array();
												purchased["Prefilled"] = false;
												purchased["TakeHome"] = false;
												purchased["InOffice"] = false;

												for(var productIndex = 0; productIndex < nextDentist.PRODUCT.length; productIndex++) {
													var nextProduct = nextDentist.PRODUCT[productIndex];
													
													if(nextProduct == "Opalescence Treswhite Supreme") {
														purchased["Prefilled"] = true; //
													} else if(nextProduct == "Opalescence PF") {
														purchased["TakeHome"] = true; //
													} else if(nextProduct == "Opalescence Boost PF") {
														purchased["InOffice"] = true; //
													}
												}
												
												if(purchased["TakeHome"]) {
													products += "<li class=\"pf\">Take Home</li>";
												} else {
													products += "<li class=\"pf hide" + (dentistCounter % 2 ? "" : " odd") + "\">Take Home</li>";
												}
												
												if(purchased["Prefilled"]) {
													products += "<li class=\"treswhite\">Prefilled Disposable</li>";
												} else {
													products += "<li class=\"treswhite hide" + (dentistCounter % 2 ? "" : " odd") + "\">Prefilled Disposable</li>";
												}
												
												if(purchased["InOffice"]) {
													products += "<li class=\"boost\">In Office</li>";
												} else {
													products += "<li class=\"boost hide" + (dentistCounter % 2 ? "" : " odd") + "\">In Office</li>";
												}
													
												products += "</ul></div>";
												
												dentistListing += products;
												dentistListing += "<div class=\"distance\">" + nextDentist.DISTANCE + " miles</div>";
												
												_address = nextDentist.ADDRESS1 + ", " + nextDentist.CITY + ", " + nextDentist.STATE + ", " +nextDentist.ZIPCODE;

												dentistListing += "<div class=\"directions\">" +
													"<a class=\"iframe getDirectionsLink\" href=\"/dentists/dentist-map.php?iframe&lat=" + _latitude + "&amp;lon=" + _longitude + "&amp;address=" + $.URLEncode(_address) + "&amp;name=" + $.URLEncode(_name) + "\"><span class=\"gdImage" + (dentistCounter % 2 ? "" : " odd") + "\"></span><span class=\"gdText\">Get Directions</span></a>";
												dentistListing += "</div></div>";
											} else {
												dentistListing += "<div class=\"dentist\">" +
												"<h6>" + nextDentist.NAME.toLowerCase() + "</h6>";
										
												dentistListing += "</div>";
												
												var formattedPhone = nextDentist.PHONE.substring(0, 3) + "-" + nextDentist.PHONE.substring(4, 7) + "-" + nextDentist.PHONE.substring(7, 11);														
												dentistListing += "<div class=\"contact\"><div>" + formattedPhone + "</div>";
												
												if(nextDentist.EMAIL) {
													dentistListing += "<div>" +
														"<a href=\"mailto:" + nextDentist.EMAIL.toLowerCase() + "\">" + nextDentist.EMAIL.toLowerCase() + "</a>" + 
														"</div>";
												}
												
												dentistListing += "</div>";
												
												if(nextDentist.ADDRESS1 && nextDentist.CITY && nextDentist.STATE && nextDentist.ZIPCODE) {
													dentistListing += "<div class=\"address\"><div>" + nextDentist.ADDRESS1.toLowerCase() + "</div>";
												
													if(nextDentist.ADDRESS2) {
														var address2IsValid = /^(STE|SUITE|ste|Ste|suite|Suite){1}/.test(nextDentist.ADDRESS2);
														
														if(address2IsValid) {
															dentistListing += "<div>" + nextDentist.ADDRESS2.toLowerCase() + "</div>";
														}
													}
													
													dentistListing += "<div>" + nextDentist.CITY.toLowerCase() + ", " + nextDentist.STATE + " " + nextDentist.ZIPCODE + "</div></div>";
													dentistListing += "<div class=\"distance\">" + nextDentist.DISTANCE + " miles</div>";
													
													_address = nextDentist.ADDRESS1 + ", " + nextDentist.CITY + ", " + nextDentist.STATE + ", " +nextDentist.ZIPCODE;
													
													dentistListing += "<div class=\"directions\">" +
														"<a class=\"iframe getDirectionsLink\" href=\"/dentists/dentist-map.php?iframe&amp;lat=" + _latitude + "&amp;lon=" + _longitude + "&amp;address=" + $.URLEncode(_address) + "&amp;name=" + $.URLEncode(_name) + "\"><span class=\"gdImage" + (dentistCounter % 2 ? "" : " odd") + "\"></span><span class=\"gdText\">Get Directions</span></a>";
												}
													
												dentistListing += "</div></div>";
											}
											
											nextPage += dentistListing;
											
											dentistCounter++;
										}
									}
									
									nextPage += "</div>";
									
									$(nextPage).appendTo("#dentistPages");
									
									nextPage = null;
									
									$("a.getDirectionsLink").fancybox({
										width: 670,
										height: 420
									});

									$("<div id=\"pages\"></div>").appendTo("#paginationControl");
									
									var controlBox = "<div id=\"controlBox1\"></div>";
									
									$(controlBox).appendTo("#resultContainer");
									
									var paginationControl = "<div id=\"paginationControl1\" class=\"paginationControl\"></div>";
									
									$(paginationControl).appendTo("#controlBox1");

									var sortControl = "<div class=\"sortByBox\"><label for=\"sortBy1\">Sort By:</label><select id=\"sortBy1\" name=\"sortBy1\"><option value=\"distance\">Distance</option></select></div>";
									
									$(sortControl).appendTo("#controlBox1");

									$("<div id=\"pages1\"></div>").appendTo("#paginationControl1");
									
									if(pageCounter > 1) {
										$("<a id=\"pageLast\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickLastPage(); return false;\">&gt;&gt;</a><span>&nbsp;</span>").appendTo("#pages");
										$("<a id=\"pageNext\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickNextPage(); return false;\">&gt;</a><span>&nbsp;</span>").appendTo("#pages");
										$("<a id=\"pageLast\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickLastPage(); return false;\">&gt;&gt;</a><span>&nbsp;</span>").appendTo("#pages1");
										$("<a id=\"pageNext\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickNextPage(); return false;\">&gt;</a><span>&nbsp;</span>").appendTo("#pages1");
									}
									
									for(pageIndex = pageCounter; pageIndex > 0; pageIndex--) {
										if(pageIndex < pageCounter) {
											$("<span>&nbsp;</span>").appendTo("#pages");
											$("<span>&nbsp;</span>").appendTo("#pages1");
										}
										
										pageControl = "<a id=\"pagePicker" + pageIndex + "\" class=\"pagePickerNumber" +  ((pageIndex == 1) ? " selected" : "") + "\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickPage(" + pageIndex + "); return false;\">" + pageIndex + "</a>";
										$(pageControl).appendTo("#pages");
										pageControl1 = "<a id=\"pagePicker1-" + pageIndex + "\" class=\"pagePickerNumber" +  ((pageIndex == 1) ? " selected" : "") + "\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickPage(" + pageIndex + "); return false;\">" + pageIndex + "</a>";
										$(pageControl1).appendTo("#pages1");
									}
									
									if(pageCounter > 1) {
										$("<span>&nbsp;</span><a id=\"pagePrev\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickPreviousPage(); return false;\">&lt;</a>").appendTo("#pages");
										$("<span>&nbsp;</span><a id=\"pageFirst\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickFirstPage(); return false;\">&lt;&lt;</a>").appendTo("#pages");
										$("<span>&nbsp;</span><a id=\"pagePrev\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickPreviousPage(); return false;\">&lt;</a>").appendTo("#pages1");
										$("<span>&nbsp;</span><a id=\"pageFirst\" class=\"pagePicker\" href=\"#\" onclick=\"$Ultradent.Opalescence.pickFirstPage(); return false;\">&lt;&lt;</a>").appendTo("#pages1");
									}
									
									$("<div class=\"numListingsSelector\"><label for=\"numListings\">Display:</label><select id=\"numListings\" name=\"numListings\" onchange=\"$Ultradent.Opalescence.changeViewAndPostback();\"><option value=\"25\">25 Listings</option><option value=\"50\">50 Listings</option><option value=\"75\">75 Listings</option></select></div>").appendTo("#paginationControl");
									$("<div class=\"numListingsSelector\"><label for=\"numListings1\">Display:</label><select id=\"numListings1\" name=\"numListings1\" onchange=\"$Ultradent.Opalescence.changeViewAndPostback();\"><option value=\"25\">25 Listings</option><option value=\"50\">50 Listings</option><option value=\"75\">75 Listings</option></select></div>").appendTo("#paginationControl1");
									$("#numListings").val($("#limit").val());
									$("#numListings1").val($("#limit").val());
								} else if(this.localJson.consumerError) {
									clearTimeout(_timeOut);
									$("#searching").remove();
									$("<h2 class=\"text_red_title consumerError\">" + this.localJson.consumerError + "</h2>").appendTo("#resultsPlaceHolder");
								} else {
									clearTimeout(_timeOut);
									$("#searching").remove();
									$("<h2 class=\"text_red_title\">Sorry, no dentists were found.</h2>").appendTo("#resultsPlaceHolder");
								}
							}, function(json) {
								$Ultradent.setJson(json);

								clearTimeout(_timeOut);
								$("#searching").remove();
								$("<h2 class=\"text_red_title\">Sorry, no dentists were found.</h2>").appendTo("#resultsPlaceHolder");
							});
						}
					}, function(json) {
						$Ultradent.setJson(json);

						clearTimeout(_timeout);
						$("#searching").remove();
						$("<h2 class=\"text_red_title\">There was a problem determining the area surrounding the given radius for your zip code.</h2>").appendTo("#resultsPlaceHolder");
					});
				} else {
					clearTimeout(_timeOut);
					$("#searching").remove();
					$("<h2 class=\"text_red_title\">The zip code you have entered is either not valid or not in our databases.</h2>").appendTo("#resultsPlaceHolder");
				}
			}, function(json) {
				$Ultradent.setJson(json);

				clearTimeout(_timeOut);
				$("#searching").remove();
				$("<h2 class=\"text_red_title\">The zip code you have entered is either not valid or not in our databases.</h2>").appendTo("#resultsPlaceHolder");
			});
		},
		
		/**
		 * displayGoogleMapsWindow displays directions to a particular dentist based in his address.
		 * @param {string zipCode
		 * @param {string} address
		 */
		displayGoogleMapsWindow: function(zipCode, address) {
			window.open("http://maps.google.com/?saddr=" + zipCode + "&daddr=" + address);
		},
		
		setNumberOfPages: function(num) {
			_numberOfPages = num;
		},
		
		getNumberOfPages: function() {
			return _numberOfPages;
		},
		
		/**
		 * pickPage picks the given page of dentists and displays it.
		 * @param {int} selectedPageNumber
		 */
		pickPage: function(selectedPageNumber) {
			if($(".page.visible").attr("id") != "page" + selectedPageNumber) {
				$(".page.visible").attr("class", "page hidden");
				$(".pagePickerNumber.selected").attr("class", "pagePickerNumber");
			}
			
			$("#page" + selectedPageNumber).attr("class", "page visible");
			$("#pagePicker" + selectedPageNumber).attr("class", "pagePickerNumber selected");
			$("#pagePicker1-" + selectedPageNumber).attr("class", "pagePickerNumber selected");
		},
		
		/**
		 * pickNextPage picks the next page of results if there is one.
		 */
		pickNextPage: function() {
			numPages = $Ultradent.Opalescence.getNumberOfPages();
			currentPage = parseInt(($(".pagePickerNumber.selected").attr("id")).substring("pagePicker".length));
			
			if(currentPage < numPages) {
				currentPage++;
				
				$Ultradent.Opalescence.pickPage(currentPage);
			}
		},
		
		/**
		 * pickPreviousPage picks the previous page of results if there is one.
		 */
		pickPreviousPage: function() {
			currentPage = parseInt(($(".pagePickerNumber.selected").attr("id")).substring("pagePicker".length));
			
			if(currentPage > 1) {
				currentPage--;
				
				$Ultradent.Opalescence.pickPage(currentPage);
			}
		},
		
		/**
		 * Picks the last page.
		 */
		pickLastPage: function() {
			numPages = $Ultradent.Opalescence.getNumberOfPages();
			currentPage = parseInt(($(".pagePickerNumber.selected").attr("id")).substring("pagePicker".length));
			
			if(currentPage < numPages) {
				$Ultradent.Opalescence.pickPage(numPages);
			}
		},
		
		/**
		 * Picks the first page.
		 */
		pickFirstPage: function() {
			currentPage = parseInt(($(".pagePickerNumber.selected").attr("id")).substring("pagePicker".length));
			
			if(currentPage > 1) {
				$Ultradent.Opalescence.pickPage(1);
			}
		},
		
		/**
		 * Changes the number of dentists to view on a single page and then posts back the information.
		 */
		changeViewAndPostback: function() {
			value = $("#numListings").val();
			
			$("#limit").val(value);
			$('#doctor_search').submit();
		},
		
		/**
		 * Map object.
		 */
		Map: function(lat, lon, address, name) {
			
			this._lat = lat;
			this._lon = lon;
			this._destAddress = address;
			this._destName = name;
			var directionsDisplay;
			var map;
			var directionsService;
			/**
			 * Renders the map.
			 */
			this.render = function() {
				if(google.maps) {
					var latlng = new google.maps.LatLng(this._lat, this._lon);
					var options = {
						zoom: 15,
						center: latlng,
						mapTypeId: google.maps.MapTypeId.ROADMAP
					};
					map = new google.maps.Map(document.getElementById("googleMapsContainer"), options);
					directionsDisplay = new google.maps.DirectionsRenderer();
					directionsService = new google.maps.DirectionsService();
					var loc = window.location.toString();
					
					document.getElementById("end_addr").innerHTML = this._destAddress;
					var contentString =	
						'<div id="content">' +
						'<div id="siteNotice"></div>' +
						'<h1 id="firstHeading" class="firstHeading">' + this._destName + '</h1>' +
						'<p>' + this._destAddress + '</p>' +
						/*'<div id="get_directions"><a>Get Directions: </a></div>' +
						'<p><b>Start: </b><input type="textbox" id="txt_start_address"/></p>'+
						//End was here
						'<input type="button" onclick="$Ultradent.Opalescence.getDirections()" id="btn_get_directions" value="Get Directions"/>' +*/
						'</div>';

						/* This was how it was before the change in look.
						'<div id="content">' +
						'<div id="siteNotice"></div>' +
						'<h1 id="firstHeading" class="firstHeading">' + this._destName + '</h1>' +
						'<h4>Address:</h4>' +
						'<p>' + this._destAddress + '</p>' + //Adding my part
						'<div id="get_directions"><a>Get Directions</a></div>' +
						'<div id="lbl_start_addr">Start Address</div>' +
						'<div id="address_div"><input type="textbox" id="txt_start_address"/></div>' +
						'<input type="button" onclick="$Ultradent.Opalescence.getDirections()" id="btn_get_directions" value="Go"/>' + //end my part
						'<img class="smallLogo" alt="Opalescence tooth whitening" src="/images/opalescence-logo-small.png" />' +
						'</div>';*/
					var infoWindow = new google.maps.InfoWindow({
						content: contentString
					});

					if (loc.search('&from') != -1)
					{
					var start = loc.search('&from=');
					directionsDisplay.setMap(map);
					directionsDisplay.setPanel(document.getElementById("googleMapsDirections"));
					var originAddr = unescape(window.location.href.slice(start+6));
					var request = {
						origin: originAddr,
						destination: address,
						travelMode: google.maps.DirectionsTravelMode.DRIVING
					};
					directionsService.route(request, function(result, status) {
						if (status == google.maps.DirectionsStatus.OK) { //If valid directions, display results
							directionsDisplay.setDirections(result);
							infoWindow.setPosition(latlng);
							infoWindow.open(map);
							}
						else //Display the marker position since there are no directions available.
						{
							var marker = new google.maps.Marker({
								position: latlng,
								map: map,
								title: "" + this._destName
							});
					
							google.maps.event.addListener(marker, "click", function() {
							infoWindow.open(map, marker);
							});
					
							infoWindow.open(map, marker);
						}
						});
					}
					else //No from, show marker.
					{
					var marker = new google.maps.Marker({
						position: latlng,
						map: map,
						title: "" + this._destName
					});
					
					google.maps.event.addListener(marker, "click", function() {
						infoWindow.open(map, marker);
					});
					
					infoWindow.open(map, marker);

					}

				} else {
					alert("There is a problem with Google Maps or with our service. Please advise us of this problem.");
				}
			
			}
		}
	
	}
	
	Ultradent.prototype.Opalescence = new Opalescence();
}

window.$Ultradent = new Ultradent();