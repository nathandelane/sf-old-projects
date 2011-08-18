package net.phyer.systems.messaging.teamchat;

public enum ServerStatus {

  RUNNING("Running"),
  STOPPED("Stopped"),
  RESTARTING("Restarting"),
  STOPPING("Stopping");

  private String status;

  ServerStatus(final String status) {
    this.status = status;
  }

  public String getStatus() {
    return this.status;
  }

}
