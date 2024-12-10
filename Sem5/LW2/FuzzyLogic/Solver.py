# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, отвечающий за реализацию функции вычисления ответа
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


from Models.MainEquation import MainEquation
from FuzzyLogic.FuzzySet import FuzzySet
from Models.Enums import Operations
from Models.SystemOfEquations import SystemOfEquations


def calculate_answer(fuzzy_set: FuzzySet, predicate):
    system = SystemOfEquations()

    for consequent in fuzzy_set:
        equation = MainEquation(consequent[0], predicate, consequent[1])
        subsystem = SystemOfEquations(equation, Operations.OR)
        system.equations.append(subsystem)

    # for eq in system.equations:
    #     for eq2 in eq.equations:
    #         for eq3 in eq2.equations:
    #             print(eq3)
    #         print()

    return system.calculate_answers()
