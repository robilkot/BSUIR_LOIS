# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление нечеткого предиката
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


import json

from .FuzzyValue import FuzzyValue
from .Predicate import Predicate
from .FuzzySet import FuzzySet
from Equations import MainEquation, SystemOfEquations


class FuzzyEntityController:
    @staticmethod
    def get_text_of_fuzzy_element(fuzzy_element):
        return f"<{fuzzy_element[0]}, {fuzzy_element[1].value}>"

    @staticmethod
    def get_text_of_fuzzy_pair(fuzzy_pair):
        return f"<<{fuzzy_pair[0][0]}, {fuzzy_pair[0][1]}>, {fuzzy_pair[1].value}>"

    @staticmethod
    def get_text_of_fuzzy_set(fuzzy_set, inline=False):
        separator = " " if inline else "\n"
        return "{" + separator + ", ".join(
            FuzzyEntityController.get_text_of_fuzzy_element(element) for element in fuzzy_set) + separator + "}"

    @staticmethod
    def get_text_of_fuzzy_predicate(fuzzy_predicate, inline=False):
        separator = " " if inline else "\n"
        return "{" + separator + ", ".join(
            FuzzyEntityController.get_text_of_fuzzy_pair(element) for element in fuzzy_predicate) + separator + "}"

    @staticmethod
    def read_sets_from_file(filename):
        sets = dict()
        with open(filename, 'r') as file:
            data = json.load(file)
            for set_name, set_ in data.items():
                new_set = FuzzySet()
                for name, value in set_:
                    new_set.add(name, FuzzyValue(value))
                sets[set_name] = sorted(new_set)
        return sets

    @staticmethod
    def read_predicates_from_file(filename):
        predicates = dict()
        with open(filename, 'r') as file:
            data = json.load(file)
            for pred_name, pred in data.items():
                new_pred = Predicate(predicate=pred)
                predicates[pred_name] = new_pred
        return predicates

    @staticmethod
    def calculate_ancedent(consequent, predicate):
        for i in consequent:
            for j in predicate:
                if j[0][1] != i[0]:
                    continue

    @staticmethod
    def calculate_answer(consequent, predicate):
        main_system_of_equations = SystemOfEquations.SystemOfEquations("and")
        for consequen in consequent:
            main_equation = MainEquation.MainEquation(consequen[0], predicate, consequen[1])
            system_of_equations = SystemOfEquations.SystemOfEquations("or")
            system_of_equations.initialize(main_equation)
            main_system_of_equations.add_system(system_of_equations)
        return main_system_of_equations.calculate_answers()
