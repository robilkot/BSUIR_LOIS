# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, содержащий класс, который отвечает за представление нечеткого предиката
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from FuzzyLogic.FuzzySet import FuzzySet
from FuzzyLogic.FuzzyValue import FuzzyValue


def get_text_of_fuzzy_pair(fuzzy_pair: tuple[tuple[str, str], FuzzyValue]):
    return f"<<{fuzzy_pair[0][0]}, {fuzzy_pair[0][1]}>, {fuzzy_pair[1].value}>"


class Predicate(FuzzySet):
    def __init__(self, predicate=None):
        super().__init__()

        if predicate:
            for i, j in predicate:
                self.add((i[0], i[1]), FuzzyValue(j))

    def __str__(self):
        return "{ " + ", ".join(get_text_of_fuzzy_pair(element) for element in self) + " }"
