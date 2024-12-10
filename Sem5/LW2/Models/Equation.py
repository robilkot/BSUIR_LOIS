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

from dataclasses import dataclass
from typing import Callable

from FuzzyLogic.FuzzyValue import FuzzyValue
from Models.Answer import Answer
from Models.Operations import equality, less_equal


@dataclass
class Equation:
    x_name: str
    x: FuzzyValue
    y: FuzzyValue
    binary_operator: Callable

    def calculate_answers(self) -> Answer:
        return self.binary_operator(self.x.value, self.y.value, self.x_name)

    def __str__(self) -> str:
        op = "==" if self.binary_operator == equality else "<="
        return f"max(0, {self.x_name} + {self.x} - 1) {op} {self.y}"
