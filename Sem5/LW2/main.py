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

import FuzzyLogic.FuzzyValue
from FuzzyLogic import Tester, FuzzyEntityController



fuzzy_entity_controller = FuzzyEntityController.FuzzyEntityController()

if __name__ == "__main__":
    tester = Tester.Tester()
    all_sets = fuzzy_entity_controller.read_sets_from_file("sets.json")
    all_predicates = fuzzy_entity_controller.read_predicates_from_file("predicates.json")

    if len(all_sets) != len(all_predicates):
        print("Ошибка: количество множеств и предикатов не совпадает.")
    else:
        for (set_name, fuzzy_set), (predicate_name, predicate) in zip(all_sets.items(), all_predicates.items()):
            print(f"Обработка для множества '{set_name}' и предиката '{predicate_name}':")
            print(set_name, ":", fuzzy_entity_controller.get_text_of_fuzzy_set(fuzzy_set))
            print(predicate_name, ":", fuzzy_entity_controller.get_text_of_fuzzy_predicate(predicate))
            answers = fuzzy_entity_controller.calculate_answer(fuzzy_set, predicate)
            print(f"'{set_name}' '{predicate_name}': {answers}")
