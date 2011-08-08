package com.nathandelane.education;

public enum Operation {

    ADDITION("+"),
    SUBTRACTION("-"),
    MULTIPLICATION("*"),
    DIVISION("/");

    private String value;

    private Operation(String value) {
	this.value = value;
    }

    @Override
    public String toString() {
	return this.value;
    }

}
