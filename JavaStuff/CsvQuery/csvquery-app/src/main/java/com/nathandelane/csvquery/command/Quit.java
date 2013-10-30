package com.nathandelane.csvquery.command;

import java.math.BigInteger;
import java.util.Map;

import org.pojomatic.Pojomatic;
import org.pojomatic.annotations.AutoProperty;

@AutoProperty
public class Quit implements ICommand {

  public static final Quit DEFAULT = new Quit();

  private final BigInteger serialVersionUid = new BigInteger("11f28be818ad4bfe9e0eaefb216b9ad7", 16);

  public String[] getParameterNames() {
    return null;
  }

  public void execute(Map<String, String> parameters) {
    // No-op
  }

  public BigInteger getServialVersionUid() {
    return serialVersionUid;
  }

  @Override
  public boolean equals(Object other) {
    return Pojomatic.equals(this, other);
  }

  @Override
  public int hashCode() {
    return Pojomatic.hashCode(this);
  }

  @Override
  public String toString() {
    return Pojomatic.toString(this);
  }

}
