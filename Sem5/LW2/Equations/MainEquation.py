# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление нечеткого уравнения
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from .Equation import *
from FuzzyLogic import Predicate


class MainEquation(dict):
    def __init__(self, consequent_name: str, predicates: Predicate.Predicate, consequent_value: FuzzyValue):
        super().__init__()
        self.consequent_name: str = consequent_name
        self.consequent_value: FuzzyValue = consequent_value

        self.upper_operator: str = "max"
        self.lower_operator: str = "max"

        for predicate in predicates:
            if predicate[0][1] != consequent_name:
                continue
            self[predicate[0][0]] = predicate[1]
