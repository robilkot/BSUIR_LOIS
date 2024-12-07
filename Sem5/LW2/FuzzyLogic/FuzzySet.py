# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление нечеткого множества
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.
from FuzzyLogic.FuzzyValue import FuzzyValue


class FuzzySet(list):
    def add(self, element, fuzzy_value: FuzzyValue) -> None:
        for item in self:
            if item[0] == element:
                return

        super().append((element, fuzzy_value))

    def __str__(self):
        return "{ " + ", ".join(f'<{element[0]}, {element[1]}>' for element in self) + " }"
