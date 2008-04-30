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
										<tr>
											<td>
											</td>
											<td>
											</td>
											<td>
											</td>
											<td>
											</td>
										</tr>
										<tr>
											<td>
											</td>
											<td>
											</td>
											<td>
											</td>
											<td>
											</td>
										</tr>
										<tr>
											<td>
											</td>
											<td>
											</td>
											<td>
											</td>
											<td>
											</td>
										</tr>
									</table>
								</div>
							</div>
						</div>
					</div>