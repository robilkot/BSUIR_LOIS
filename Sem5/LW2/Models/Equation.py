# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл класса, реализующего модель уравнения
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from dataclasses import dataclass

from FuzzyLogic.FuzzyValue import FuzzyValue
from Models.Answer import Answer
from Models.Operations import Operation, Solvable


@dataclass
class Equation(Solvable):
    variable: str
    x: FuzzyValue
    y: FuzzyValue
    solve_operation: Operation

    def solve(self) -> Answer:
        # print(self)
        return self.solve_operation(self.x.value, self.y.value, self.variable)

    def __str__(self) -> str:
        return f"max(0, {self.variable} + {self.x} - 1) {self.solve_operation} {self.y}"
