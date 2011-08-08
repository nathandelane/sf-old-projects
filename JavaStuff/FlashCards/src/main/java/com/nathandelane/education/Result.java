package com.nathandelane.education;

import java.util.Date;

public final class Result {

    private Date totalTime;
    private PassedOrFailed passedOrFailed;

    public Result(Date totalTime, PassedOrFailed passedOrFailed) {
	this.totalTime = totalTime;
	this.passedOrFailed = passedOrFailed;
    }

    public Date getTotalTime() {
	return this.totalTime;
    }

    public PassedOrFailed getPassedOrFailed() {
	return this.passedOrFailed;
    }

}
