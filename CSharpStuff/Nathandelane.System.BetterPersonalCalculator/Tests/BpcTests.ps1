# Copyright (C) 2009, Nathandelane.
# License:
# Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.
# 
# This program is free software; you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation; either version 3, or (at your option)
# any later version.
# 
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
# 02111-1307, USA.

Write-Output ""
Write-Output "BPC Tests - PC.NET"
Write-Output "This script comprises a series of tests to be run against BPC - Better Personal Calculator."
Write-Output "This script's version is 2.0 and should be used against builds 1.3.0.0 and higher of BPC."

$anyFailed = $False
$numberFailed = 0
$expressions = "e # Constant E", "pi # Constant PI", "1 + 2 + 3 + 4 + 5 # Simple Addition", "15 - 1 - 2 - 3 - 4 - 5 # Simple Subtraction", "13 / (1 / 2) # Simple Division", "6 * 8 * 2 # Simple Multiplication", "(12 + 2) * 6 / 7 - 3 # Arithmetic", "cos(pi) # Trigonometry - Radians", "--mode-degrees tan(45) # Trigonometry - degrees", "--mode-degrees atan(1)", "237 % 15 # Algebra - Modulus", "237 // 15 # Algrebra - DivMod", "6! # Algebra - Factorial", "log(pi) # Algebra - Logarithm, base-10", "ln(pi) # Natural Logarithm", "lb(pi) # Binary Logarithm, Base-2", "rad(45) # To Radians", "deg(0.785398163397448) # To Degrees", "sqrt(25) # Square Root", "fh + ch # Hexadecimal", "10o - 7o # Octal", "1001b + 101b # Binary", "toh(24) # Conversion - To Hex", "too(24) #Conversion - To Oct", "tob(24) # Conversion - Binary", "tod(18h) # Conversion - To Dec", "-(13 + 19 * 2) # Negative Distribution", "!13 + 19 * 2 # Binary NOT", "1001b ^ 110b # Binary XOR", "110b | 1b # Binary - OR", "true | false # Boolean", "true != false # Boolean Conditional", "false == false # Boolean Conditional", "24 > 13 # Numeric Conditional", "19 <= 20 # Numeric Conditional", "2 != (4 / 2) # Conditional Expressions", "2 ** 3 # Algebra - Power Function", "13 ** 2; / 12 # Multi-expression and Implicit Last Result", "13 ** 2; $ # Last Result, $", "2 ** 2; **2; **2 # Multi-expression and Implicit Last Result", "_var = (13 * 2); 12 ** 2; $ + _var # Multi-expression and Variables", "_myNumber = 18; _myNumber # Variables"
$expected = [Math]::E, [Math]::PI, 15, 0, 26, 96, 9, -1, 1, 45, 12, 15, 720, 0.497149872694133, 1.1447298858494, 1.65149612947232, 0.785398163397448, 45, 5, "1bh", "1o", "1110b", "18h", "30o", "11000b", 24, -51, 25, "1111b", "111b", "true", "true", "true", "true", "true", "false", 8, "169 14.0833333333333", "169 169", "4 16 256", "26 144 170", "18 18"

# Assert whether two string values are equal to each other and return a string formatted result.
function AssertEquals([string]$expected, [string]$actual)
{
	$ex = $expected
	$ac = $actual

	if($ac -eq $ex)
	{
		return "Passed"
	}
	
	return "Failed"
}

if($expressions.Length -eq $expected.Length)
{
	Write-Output ""

	$bpcVersion = ([string](bpc --version)).Split(":")[1].Trim()

	Write-Output "Current version of PC.NET is $bpcVersion"
	Write-Output "Running command-line test suite for PC.NET..."
	Write-Output ""
	Write-Output "bpc <expression> = expected (actual)...Pass or Fail"
	Write-Output ""

	$numberOfTests = $expressions.Length
	$expressionIndex = 0

	do
	{
		$expr = $expressions[$expressionIndex]
		$bpcResult = (bpc $expr.Split(" "))
		$expct = $expected[$expressionIndex]
		$result = AssertEquals $expct $bpcResult
		Write-Output "bpc $expr = $expct ($bpcResult)...$result"
		
		if($result -eq "Failed")
		{
			$anyFailed = $True
			$numberFailed++
		}
		
		$expressionIndex++
	}
	while($expressionIndex -lt $numberOfTests)

	Write-Output ""
	Write-Output "Tests completed."
	Write-Output "$numberOfTests tests were run."
	Write-Output ""

	$numberPassed = $expressions.Length - $numberFailed

	Write-Output "$numberPassed tests passed."
	Write-Output "$numberFailed tests failed."
	Write-Output ""
}
else
{
	Write-Output ""
	Write-Output "An error occurred: the number of expected results do not match the number of expressions available."
	Write-Output ""
}
