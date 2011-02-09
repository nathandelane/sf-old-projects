<?php

require_once(dirname(__FILE__) . "/_sign-up-information.inc.php");

$page = new _Sign_Up_Information_Page();
$page->openDocument();

?>
<div class="contentInner">
	<h1>Sign Up for PhyleBox</h1>
	<div class="panel">
		<div class="panelInner">
			<form id="signUpForm" name="signUpForm" action="<?php echo $_SERVER["REQUEST_URI"]; ?>" method="post" enctype="application/x-www-form-urlencoded">
				<input type="hidden" id="token" name="token" value="<?php echo $page->createToken(); ?>" />
				<p>
					<span style="font-style: italic;">Indicates that the field or fields are required:</span> 
					<span class="requiredColor">*</span>
				</p>
				<table border="0" id="registrationFormTable">
					<tbody>
						<tr>
							<td class="label"></td>
							<td></td>
							<td>
								Please choose a unique username. In order to ensure that your chosen username is available please click on the control on the right side of the input.
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="username">
									Username: 
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<input type="text" name="username" id="username" value="" />
								<a href="javascript: void(0);" id="checkUserNameLink">Check availability</a>
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="password">
									Password: 
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<input type="password" name="password" id="password" value="" />
								<input type="password" name="repeatPassword" id="repeatPassword" value="" />
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="explicitness">
									Explicitness: 
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<fieldset>
									<a href="javascript: void(0);" id="whyExplicityLink">What is this?</a>
<?php

$page->renderExplicitness();

?>
								</fieldset>
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="dateOfBith">
									Date of Birth: 
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<input type="text" name="datOfBirth" id="dateOfBirth" value="" />
								<a href="javascript: void(0);" id="whyDateOfBirthIsRequiredLink">Why is this required?</a>
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="firstRealName">
									Real Name: 
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<input type="text" name="firstRealName" id="firstRealName" value="" />
								<input type="text" name="lastRealName" id="lastRealName" value="" />
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="emailAddress">
									Email Address: 
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<input type="text" name="emailAddress" id="emailAddress" value="" />
								<input type="text" name="repeatEmailAddress" id="repeatEmailAddress" value="" />
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="country">
									Country:
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<select name="country" id="country">
									<option value="0">Select your Country</option>
									<option value="10001">-- Country not listed</option>
<?php

$page->renderCountryOptions();

?>
								</select>
								<input type="text" name="otherCountry" id="otherCountry" value="" style="display: none;" />
							</td>
						</tr>
						<tr>
							<td class="label">
								<label for="state">
									State/Province:
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<input type="text" name="state" id="state" value="" />
							</td>							
						</tr>
						<tr>
							<td class="label">
								<label for="city">
									City:
								</label>
							</td>
							<td>
								<span class="requiredColor">*</span>
							</td>
							<td>
								<input type="text" name="city" id="city" value="" />
							</td>							
						</tr>
						<tr>
							<td class="label">
								<label for="bio">
									Bio: 
								</label>
							</td>
							<td></td>
							<td>
								<textarea name="bio" id="bio"></textarea>
							</td>
						</tr>
					</tbody>
				</table>
				<div class="formRow">
					<input id="submitButton" type="submit" value="Continue to Upload Avatar >>" />
				</div>
			</form>
		</div>
	</div>
</div>
<?php

$page->closeDocument();

?>