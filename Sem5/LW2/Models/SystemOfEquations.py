# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, реализующий класс, который отвечает за представление системы нечетких уравнений
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from Models.Enums import Operations
from .Equation import *
from .Answer import Answer
from .MainEquation import MainEquation


class SystemOfEquations:
    def __init__(self, equation: MainEquation | None = None, type: Operations = Operations.AND):
        self.type = type
        self.equations: list = list()

        if equation:
            self.initialize(equation)

    def initialize(self, main_equation):
        for key in main_equation.keys():
            new_system = SystemOfEquations()

            for variable, value in main_equation.items():
                comparer = equality if key == variable else less_equal
                equation = Equation(variable, value, main_equation.consequent_value, comparer)
                new_system.equations.append(equation)

            self.equations.append(new_system)

    def calculate_answers(self) -> Answer:
        answers = Answer(type_of_answer=self.type)

        for equation in self.equations:
            answer = equation.calculate_answers()
            answers.add_answer(answer)

        return answers
