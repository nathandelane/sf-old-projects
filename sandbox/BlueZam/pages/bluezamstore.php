					<div id="storeContainer" class="storeContainer">
						<div id="titleBar" class="storeTopBarContainer">
							<div id="userStoreHeading" class="headingMajor">USER STORE</div>
						</div>
						<div id="storePanelContainer" class="storePanelContainer">
							<div id="storeControlPanel" class="storeControlPanel">
								<div id="row1" class="controlPanelRow">
									<div id="row1column1" class="controlPanelColumn floatLeft">
										<label id="sortByLabel" class="controlPanelLabel" for="sortBySelector">Sort by:</label>
										<select id="sortBySelector" class="controlSelector" name="sortBySelector">
											<option value="relevance">Relevance</option>
											<option value="price">Price</option>
										</select>
									</div>
									<div id="row1column2" class="controlPanelColumn floatRight">
										<label id="viewLabel" class="controlPanelLabel floatLeft" for="viewSelector">View:</label>
										<select id="viewSelector" class="controlSelector floatLeft" name="viewSelector">
											<option value="relevance">12 items per page</option>
											<option value="price">16 items per page</option>
										</select>
										<div id="pagingControls" class="pagingControls floatLeft">
											<a id="viewAllLink" class="viewAllLink" href="">View All</a>
											<?
												// Here needs to go the number of pages of stuff that we have. Temporarily I have created two links after this section.
												// - Nathan Lane
											?>
											<a id="page1" class="pagingLink" href="">1</a>
											<a id="page1" class="pagingLink" href="">2</a>
										</div>
									</div>
								</div>								
								<div id="row2" class="controlPanelRow">
									<div id="row2column1" class="controlPanelColumn floatLeft">
										<label id="refineByLabel" class="controlPanelLabel" style="font-weight:bold;">Refine by:</label>
									</div>
								</div>
							</div>
							<div id="storePage" class="storePage">
								<div id="storePageInner" class="storePageInner">
									<table id="storePageTable" class="storePageTable" name="storePageTable">
										<?php
											// This would contain some code base on the inventory we have.
											// - Nathan Lane
										?>
										<tr>
											<td>
												<div id="itemContainer1" class="itemContainer">
													<div id="itemImage1" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtblack.jpeg"/>
													</div>
													<div id="itemDetails1" class="itemDetails">
														<div id="description1" class="description">Blue Zam original apparell shirt Black.</div>
														<div id="originalPrice1" class="originalPrice">orig. $20.00</div>
														<div id="salePrice1" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer2" class="itemContainer">
													<div id="itemImage2" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtwhite.jpeg"/>
													</div>
													<div id="itemDetails2" class="itemDetails">
														<div id="description2" class="description">Blue Zam original apparell shirt White.</div>
														<div id="originalPrice2" class="originalPrice">orig. $20.00</div>
														<div id="salePrice2" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer3" class="itemContainer">
													<div id="itemImage3" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellhoodieblack.jpeg"/>
													</div>
													<div id="itemDetails3" class="itemDetails">
														<div id="description3" class="description">Blue Zam original apparell hoodie Black.</div>
														<div id="originalPrice3" class="originalPrice">orig. $20.00</div>
														<div id="salePrice3" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer4" class="itemContainer">
													<div id="itemImage4" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtblack.jpeg"/>
													</div>
													<div id="itemDetails4" class="itemDetails">
														<div id="description4" class="description">Blue Zam original apparell shirt Black.</div>
														<div id="originalPrice4" class="originalPrice">orig. $20.00</div>
														<div id="salePrice4" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<td>
												<div id="itemContainer5" class="itemContainer">
													<div id="itemImage5" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtwhite.jpeg"/>
													</div>
													<div id="itemDetails5" class="itemDetails">
														<div id="description5" class="description">Blue Zam original apparell shirt White.</div>
														<div id="originalPrice5" class="originalPrice">orig. $20.00</div>
														<div id="salePrice5" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer6" class="itemContainer">
													<div id="itemImage6" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellhoodieblack.jpeg"/>
													</div>
													<div id="itemDetails6" class="itemDetails">
														<div id="description6" class="description">Blue Zam original apparell hoodie Black.</div>
														<div id="originalPrice6" class="originalPrice">orig. $20.00</div>
														<div id="salePrice6" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer7" class="itemContainer">
													<div id="itemImage7" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtblack.jpeg"/>
													</div>
													<div id="itemDetails7" class="itemDetails">
														<div id="description7" class="description">Blue Zam original apparell shirt Black.</div>
														<div id="originalPrice7" class="originalPrice">orig. $20.00</div>
														<div id="salePrice7" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer8" class="itemContainer">
													<div id="itemImage8" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtwhite.jpeg"/>
													</div>
													<div id="itemDetails8" class="itemDetails">
														<div id="description8" class="description">Blue Zam original apparell shirt White.</div>
														<div id="originalPrice8" class="originalPrice">orig. $20.00</div>
														<div id="salePrice8" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<td>
												<div id="itemContainer9" class="itemContainer">
													<div id="itemImage9" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellhoodieblack.jpeg"/>
													</div>
													<div id="itemDetails9" class="itemDetails">
														<div id="description9" class="description">Blue Zam original apparell hoodie Black.</div>
														<div id="originalPrice9" class="originalPrice">orig. $20.00</div>
														<div id="salePrice9" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer10" class="itemContainer">
													<div id="itemImage10" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtblack.jpeg"/>
													</div>
													<div id="itemDetails10" class="itemDetails">
														<div id="description10" class="description">Blue Zam original apparell shirt Black.</div>
														<div id="originalPrice10" class="originalPrice">orig. $20.00</div>
														<div id="salePrice10" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer11" class="itemContainer">
													<div id="itemImage11" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellshirtwhite.jpeg"/>
													</div>
													<div id="itemDetails11" class="itemDetails">
														<div id="description11" class="description">Blue Zam original apparell shirt White.</div>
														<div id="originalPrice11" class="originalPrice">orig. $20.00</div>
														<div id="salePrice11" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
											<td>
												<div id="itemContainer12" class="itemContainer">
													<div id="itemImage12" class="itemImage">
														<img id="item9" src="_images/inventoryimages/bluezamapparrellhoodieblack.jpeg"/>
													</div>
													<div id="itemDetails12" class="itemDetails">
														<div id="description12" class="description">Blue Zam original apparell hoodie Black.</div>
														<div id="originalPrice12" class="originalPrice">orig. $20.00</div>
														<div id="salePrice12" class="salePrice">sale $15.99</div>
													</div>
												</div>
											</td>
										</tr>
									</table>
								</div>
							</div>
						</div>
					</div>