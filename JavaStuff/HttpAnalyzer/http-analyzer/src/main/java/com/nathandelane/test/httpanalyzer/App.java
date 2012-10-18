package com.nathandelane.test.httpanalyzer;

import java.io.IOException;

import org.jsoup.Connection;
import org.jsoup.Connection.Response;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

public class App {

	public static void main(String[] args) throws IOException {
		if (args.length < 1) {
			System.out.println("usage: java -jar http-analyzer.jar url [css-query [comma-separated-list-of-properties-to-get]]");
		} else {
			final Connection connection = Jsoup.connect(args[0]);
			connection.timeout(360000);

			if (System.getenv("noheaders") == null) {
				final Response response = connection.execute();

				for (String headerName : response.headers().keySet()) {
					System.out.println(headerName + ": " + response.header(headerName));
				}

				System.out.println();
			}

			final Document doc = connection.get();

			if (args.length > 1) {
				final Elements elements = doc.select(args[1]);
				final int numElements = elements.size();

				int elementIndex = 0;

				while (elementIndex < numElements) {
					final Element element = elements.get(elementIndex);

					if (args.length == 3) {
						String[] properties = args[2].split(",");

						if (properties.length > 0) {
							final StringBuilder propertyBuilder = new StringBuilder();

							for (String nextProp : properties) {
								if (propertyBuilder.toString().length() > 0) {
									propertyBuilder.append(",");
								}

								propertyBuilder.append(nextProp).append("=");

								if (element.hasAttr(nextProp.toLowerCase())) {
									propertyBuilder.append("\"").append(element.attr(nextProp)).append("\"");
								} else if (nextProp.equalsIgnoreCase("text")) {
									propertyBuilder.append("text=");

									if (element.text() != null && !element.text().equals("")) {
										propertyBuilder.append("\"").append(element.text()).append("\"");
									}
								}
							}

							System.out.println(propertyBuilder.toString());
						}
					} else {
						System.out.println(element);
					}

					elementIndex++;
				}
			}
		}

		System.out.println();
	}

}
