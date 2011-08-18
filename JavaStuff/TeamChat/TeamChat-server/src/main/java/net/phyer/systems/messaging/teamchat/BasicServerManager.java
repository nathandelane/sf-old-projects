package net.phyer.systems.messaging.teamchat;

public final class BasicServerManager implements ServerManager {

  private final Server server;

  public BasicServerManager(final Server server) {
    this.server = server;
  }

  @Override
  public void start() {
    server.setStatus(ServerStatus.STARTING);
  }

  @Override
  public void stop() {
    server.setStatus(ServerStatus.STOPPING);
  }

  @Override
  public void restart() {
    server.setStatus(ServerStatus.RESTARTING);
  }

  @Override
  public ServerStatus status() {
    return server.getStatus();
  }

}
