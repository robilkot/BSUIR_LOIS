# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл классов, представляющих операции для решения уравнений
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from abc import ABC

from FuzzyLogic.Interval import Interval
from FuzzyLogic.FuzzyValue import FuzzyValue
from Models.Answer import Answer


class Operation(ABC):
    def __call__(self, *args, **kwargs):
        pass

    def __str__(self):
        pass


class Solvable(ABC):
    def solve(self) -> Answer:
        pass


class Equal(Operation):
    def __call__(self, x: float, y: float, name: str) -> Answer:
        if y == 0.0:
            return Answer({name: Interval(FuzzyValue(0.0), FuzzyValue(1.0 - x))}, None)
        elif y > x:
            return Answer(None, None)
        else:
            return Answer({name: Interval(FuzzyValue(y - x + 1.0), FuzzyValue(y - x + 1.0))}, None)

    def __str__(self) -> str:
        return "=="


class LessEqual(Operation):
    def __call__(self, x: float, y: float, name: str) -> Answer:
        return Answer({name: Interval(FuzzyValue(0.0), FuzzyValue(y - x + 1.0))}, None)

    def __str__(self) -> str:
        return "<="
