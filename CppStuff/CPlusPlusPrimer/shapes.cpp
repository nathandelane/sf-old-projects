/*
 * shapes.cpp
 *
 *  Created on: Nov 3, 2010
 *      Author: nalane
 */

#include <iostream>
#include <string>
#include <vector>
using namespace std;

/**
 * IDrawable interface.
 */
class IDrawable
{
public:
	/**
	 * Draws the IDrawable.
	 */
	virtual void Draw() = 0;
};

/**
 * Concrete Square class.
 */
class Square : public IDrawable
{
private:
	int _widthAndHeight;
public:
	/**
	 * Creates an instance of Square.
	 */
	Square(int);

	/**
	 * Draws this Square.
	 */
	void Draw();
};

/**
 * Creates an instance of Square.
 */
Square::Square(int widthAndHeight)
{
	_widthAndHeight = widthAndHeight;
}

/**
 * Draws this Square.
 */
void Square::Draw()
{
	for (int yUnitIndex = 0; yUnitIndex < _widthAndHeight; yUnitIndex++)
	{
		string row;

		if (yUnitIndex == 0 || yUnitIndex == (_widthAndHeight - 1))
		{
			row.assign(_widthAndHeight, '*');
		}
		else
		{
			string middle;
			middle.assign((_widthAndHeight - 2), ' ');

			row = "*" + middle + "*";
		}

		cout << row << endl;
	}
}

class Rectangle : public IDrawable
{
private:
	int _width;
	int _height;
public:
	/**
	 * Creates an instance of Rectangle.
	 */
	Rectangle(int, int);

	/**
	 * Draws this rectangle.
	 */
	void Draw();
};

/**
 * Creates an instance of Rectangle.
 */
Rectangle::Rectangle(int width, int height)
{
	_width = width;
	_height = height;
}

/**
 * Draws this Rectangle.
 */
void Rectangle::Draw()
{
	for (int yUnitIndex = 0; yUnitIndex < _height; yUnitIndex++)
	{
		string row;

		if (yUnitIndex == 0 || yUnitIndex == (_height - 1))
		{
			row.assign(_width, '*');
		}
		else
		{
			string middle;
			middle.assign((_width - 2), ' ');

			row = "*" + middle + "*";
		}

		cout << row << endl;
	}
}


/**
 * Program entry point.
 */
int main()
{
	vector<IDrawable *> shapes;
	shapes.push_back(new Square(8));
	shapes.push_back(new Rectangle(3, 5));

	for (vector<IDrawable *>::iterator it = shapes.begin(); it != shapes.end(); ++it)
	{
		(* it)->Draw();
	}
}
