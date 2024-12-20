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
from FuzzyLogic.FuzzySet import FuzzySet
from .Equation import *


class MainEquation(dict):
    def __init__(self, consequent_name: str, rule: FuzzySet, consequent_value: FuzzyValue):
        super().__init__()
        self.consequent_name: str = consequent_name
        self.consequent_value: FuzzyValue = consequent_value

        for key, value in rule.items():
            if key[1] != consequent_name:
                continue
            self[key[0]] = value
