# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл класса, реализующего модель системы уравнения
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from .Enums import Operations
from .Answer import Answer
from .Equation import Equation
from .EquationData import EquationData
from .Operations import Equal, Solvable, LessEqual


class SystemOfEquations(Solvable):
    def __init__(self, equation: EquationData | None = None, type_: Operations = Operations.AND):
        self.__type = type_
        self.equations: list = list()

        if equation:
            self.initialize(equation)

    def initialize(self, main_equation):
        for key in main_equation.keys():
            new_system = SystemOfEquations()

            for variable, value in main_equation.items():
                operation = Equal() if key == variable else LessEqual()
                equation = Equation(variable, value, main_equation.consequent_value, operation)
                new_system.equations.append(equation)

            self.equations.append(new_system)

    def solve(self) -> Answer:
        answers = Answer(type_of_answer=self.__type)

        for equation in self.equations:
            answer = equation.solve()
            answers.add_answer(answer)

        return answers
