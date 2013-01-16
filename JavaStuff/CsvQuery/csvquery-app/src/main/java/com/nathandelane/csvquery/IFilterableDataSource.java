package com.nathandelane.csvquery;

import java.util.Iterator;

import com.google.common.base.Predicate;

public interface IFilterableDataSource<T> {

  public Iterator<T> iterator();

  public Iterable<T> filter(Predicate<T> predicate);

  public int numberOfRows();

  public int numberOfColumns();

  public int numberOfRows(Predicate<T> predicate);

}
