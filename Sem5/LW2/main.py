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

from FuzzyLogic import FuzzyEntityController


if __name__ == "__main__":
    controller = FuzzyEntityController.FuzzyEntityController()
    all_sets = controller.read_sets_from_file("sets.json")
    all_predicates = controller.read_predicates_from_file("predicates.json")

    for (set_name, fuzzy_set), (predicate_name, predicate) in zip(all_sets.items(), all_predicates.items()):
        print(f"Обработка для множества '{set_name}' и предиката '{predicate_name}':")
        print(set_name, ":", controller.get_text_of_fuzzy_set(fuzzy_set, True))
        print(predicate_name, ":", controller.get_text_of_fuzzy_predicate(predicate, True))
        answers = controller.calculate_answer(fuzzy_set, predicate)
        print(f"'{set_name}' '{predicate_name}': {answers}")
