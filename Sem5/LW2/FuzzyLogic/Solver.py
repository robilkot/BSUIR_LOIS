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

from Models.Enums import Operations
from Models.EquationData import EquationData
from FuzzyLogic.FuzzySet import FuzzySet
from Models.SystemOfEquations import SystemOfEquations


def calculate_answer(implication: FuzzySet, rule: FuzzySet):
    main_system = SystemOfEquations()

    for name, value in implication.items():
        data = EquationData(name, value, rule)
        subsystem = SystemOfEquations(data, Operations.OR)
        main_system.equations.append(subsystem)

    return main_system.solve()
