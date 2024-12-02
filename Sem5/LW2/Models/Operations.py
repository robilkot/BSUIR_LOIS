from enum import Enum

from Equations.Answer import Answer
from FuzzyLogic.FuzzyInterval import FuzzyInterval
from FuzzyLogic.FuzzyValue import FuzzyValue


def equality(x: float, y: float, name: str):
    if y == 0.0:
        return Answer({name: FuzzyInterval(FuzzyValue(0.0), FuzzyValue(1.0 - x))}, None)
    elif y > x:
        return Answer(None, None)
    else:
        return Answer({name: FuzzyInterval(FuzzyValue(y - x + 1.0), FuzzyValue(y - x + 1.0))}, None)


def less_equal(x: float, y: float, name: str):
    return Answer({name: FuzzyInterval(FuzzyValue(0.0), FuzzyValue(y - x + 1.0))}, None)
