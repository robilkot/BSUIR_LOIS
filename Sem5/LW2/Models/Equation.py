# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, реализующий класс, который отвечает за представление нечеткого уравнения
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from typing import Callable

from FuzzyLogic.FuzzyValue import FuzzyValue
from Models.Answer import Answer
from Models.Operations import equality, less_equal


class Equation:
    def __init__(self, x_name: str, value_x: FuzzyValue, value_y: FuzzyValue, binary_operator: Callable) -> None:
        self.x_name: str = x_name

        self.value_x: FuzzyValue = value_x
        self.value_y: FuzzyValue = value_y

        self.binary_operator: Callable = binary_operator
        self.keys: set = set()
        self.keys.add(x_name)

    def calculate_answers(self) -> Answer:
        return self.binary_operator(self.value_x.value, self.value_y.value, self.x_name)

    def __str__(self) -> str:
        op = str()
        if self.binary_operator == equality:
            op = "=="
        elif self.binary_operator == less_equal:
            op = "<="
        return f"max(0, {self.x_name} + {self.value_x} - 1) {op} {self.value_y}"
