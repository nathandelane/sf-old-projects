<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(dirname(__FILE__) . "/_sign-up-account-type.inc.php");

$page = new _Sign_Up_Account_Type_Page();
$page->openDocument();

?>
<div class="contentInner">
	<h1>Sign Up for PhyleBox - Account Type</h1>
	<div class="panel">
		<div class="panelInner">
			<form id="signUpForm" name="signUpForm" action="<?php echo PhyleBox_Config::getPhyleBoxRoot() . "/sign-up-information.php"; ?>" method="post" enctype="application/x-www-form-urlencoded" onsubmit="javascript: return $Phyer.Registration.accountTypeFormIsValid();">
				<p>Please choose a hosting plan. For this beta all plans are free. Simply choose a plan that best meets your needs. Then click on the button to continue the sign up process.</p>
				<p class="important">* Only one account per user!</p>				
				<table id="accountTypeTable">
					<tbody>
						<tr>
							<td>
								<h2>Beta Hosting Plan</h2>
							</td>
							<td>
								<h2>Beta Storage Plan</h2>
							</td>
						</tr>
						<tr>
							<td class="accountTypeSection">
								<h3>Member Pass - FREE</h3>
								<ul>
									<li>250 Mb Webspace</li>
									<li>500 Mb Personal Storage</li>
									<li>Chat Access</li>
								</ul>
							</td>
							<td class="accountTypeSection">
								<h3>Data Pass - FREE</h3>
								<ul>
									<li>2.1 Gb Personal Storage</li>
								</ul>
							</td>
						</tr>
						<tr>
							<td>
								<input type="radio" name="accountType" id="accountType" value="5" />
							</td>
							<td>
								<input type="radio" name="accountType" id="accountType" value="6" />
							</td>
						</tr>
					</tbody>
				</table>
				<input type="submit" id="submitButton" value="Choose Account Type and Enter Personal Information >>" />
			</form>
		</div>
	</div>
</div>
<?php 

$page->closeDocument();

?>