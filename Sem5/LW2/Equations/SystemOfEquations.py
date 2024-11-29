# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление системы нечетких уравнений
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from .Equation import *
from .Answer import Answer


class SystemOfEquations:
    def __init__(self, type_of_system="and", list_of_systems: list = None):
        self.list_of_equations: list = list()
        self.type_of_system = type_of_system
        self.keys = set()
        if list_of_systems is None:
            return
        if not isinstance(list_of_systems, list):
            invalid_type_error(self.__init__, list_of_systems, list)
            return
        for system in list_of_systems:
            self.list_of_equations.append(system)
            self.keys.update(system.keys())

    def add_equation(self, equation):
        self.list_of_equations.append(equation)
        self.keys.update(equation.keys)

    def add_system(self, system):
        self.list_of_equations.append(system)
        self.keys.update(system.keys)

    def initialize(self, main_equation):
        for key in main_equation.keys():
            self.keys.add(key)
            temp_system_of_equations = SystemOfEquations("and")
            for x, value_x in main_equation.items():
                if key == x:
                    temp_system_of_equations.add_equation(
                        Equation(x, value_x, main_equation.consequent_value, operation["=="]))
                else:
                    temp_system_of_equations.add_equation(
                        Equation(x, value_x, main_equation.consequent_value, operation["<="]))
            self.list_of_equations.append(temp_system_of_equations)
            if len(self.list_of_equations) != len(self.keys):
                print("Невозможная операция")

    def calculate_answers(self):
        answers = Answer(type_of_answer=self.type_of_system)
        for item in self.list_of_equations:
            answer = item.calculate_answers()
            answers.add_answer(answer)
        return answers

    def __repr__(self):
        return f"{self.type_of_system} {tuple(str(equation) for equation in self.list_of_equations)}"
