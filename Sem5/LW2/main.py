# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, отвечающий за реализацию главной функции программы
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


from FuzzyLogic.Solver import calculate_answer
from Parser.Facade import parse_file

if __name__ == "__main__":
    try:
        fuzzy_set, predicate = parse_file("Input/2.kb")
        answers = calculate_answer(fuzzy_set, predicate)
        result = str(answers)
        print(result if len(result) > 0 else "No solutions found")
    except Exception as e:
        print(f"Error: {e}")
