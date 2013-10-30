package com.nathandelane.csvquery.command;

import java.math.BigInteger;
import java.util.Map;

public interface ICommand {

  public String[] getParameterNames();

  public void execute(Map<String, String> parameters);

  public BigInteger getServialVersionUid();

}
