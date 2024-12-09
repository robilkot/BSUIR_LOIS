# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Классы, отвечающие за представление результата решения нечеткого уравнения
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


from FuzzyLogic.FuzzyEntityController import calculate_answer
from Parser.Facade import parse_file

if __name__ == "__main__":
    fuzzy_set, predicate = parse_file("Input/2.kb")
    answers = calculate_answer(fuzzy_set, predicate)

    # print("set:", fuzzy_set)
    # print("pre:", predicate)
    print(answers)
