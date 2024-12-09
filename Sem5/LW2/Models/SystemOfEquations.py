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


class SystemOfEquations:
    def __init__(self, type: Operations = Operations.AND):
        self.type = type
        self.equations: list = list()
        self.keys = set()

    def add_equation(self, equation):
        self.equations.append(equation)
        self.keys.update(equation.keys)

    def add_system(self, system):
        self.equations.append(system)
        self.keys.update(system.keys)

    def initialize(self, main_equation):
        for key in main_equation.keys():
            self.keys.add(key)
            temp_system_of_equations = SystemOfEquations()
            for x, value_x in main_equation.items():
                if key == x:
                    temp_system_of_equations.add_equation(
                        Equation(x, value_x, main_equation.consequent_value, equality))
                else:
                    temp_system_of_equations.add_equation(
                        Equation(x, value_x, main_equation.consequent_value, less_equal))
            self.equations.append(temp_system_of_equations)
            if len(self.equations) != len(self.keys):
                print("Невозможная операция")

    def calculate_answers(self) -> Answer:
        answers = Answer(None, None, type_of_answer=self.type)

        for equation in self.equations:
            answer = equation.calculate_answers()
            answers.add_answer(answer)

        return answers
