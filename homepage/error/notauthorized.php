<?
	header("HTTP/1.0 401 Access Denied");
?>
<html>
	<head>
		<title>HTTP 401: Access Denied</title>
		<link rel="stylesheet" type="text/css" href="../_stylesheets/main.css"/>
	</head>
	<body class="error">
		<div id="errorPageContainer" class="errorPageContainer">
			<div id="errorPageContainerInner" class="errorPageContainerInner">
				<h3>HTTP 401 Access Denied.</h3>
				<h1>You are not authorized to view this directory.</h1>
				<p>
					The directory you are trying to access has been protected by the system administrator. We have recorded the information 
					below for our own record keeping. We do not disclose this information to our vendors, affiliates, or anybody outside 
					the technical department employment body.
				</p>
				<p>
					If you believe you reached this page in error, please feel free to contact us by clicking on the <i>helpdesk</i> link 
					below and tell us what you were trying to do at the time that you were redirected to this page.
				</p>
				<p>
					Thank you.
				</p>
				<div>
					Information Technology Department
					<div>
						<a id="helpdeskLink" href="<?php echo "helpdesk.php"; ?>">Helpdesk</a>
					</div>
				</div>
			</div>
		</div>
	</body>
</html>