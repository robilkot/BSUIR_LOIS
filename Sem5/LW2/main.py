# Практическая работа №1 по дисциплине ЛОИС
#  Вариант 1: Реализовать прямой нечёткий логический вывод, используя треугольную норму min({xi}U{yi}) и нечёткую импликацию Гёделя
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за сравнение нечётких множеств по их содержанию
#  18.10.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


from FuzzyLogic.FuzzyEntityController import calculate_answer
from Parser.Facade import parse_file

if __name__ == "__main__":
    fuzzy_set, predicate = parse_file("Input/2.kb")
    answers = calculate_answer(fuzzy_set, predicate)

    print("set:", fuzzy_set)
    print("pre:", predicate)
    print(f"ans: {answers}")
