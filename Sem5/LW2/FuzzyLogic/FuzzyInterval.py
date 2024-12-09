# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, содержащий класс, который отвечает за представление нечеткого интервала
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from __future__ import annotations

from FuzzyLogic.FuzzyValue import FuzzyValue


class FuzzyInterval:
    def __init__(self, lower_border: FuzzyValue, upper_border: FuzzyValue) -> None:
        if upper_border < lower_border:
            upper_border, lower_border = lower_border, upper_border

        self.lower_border: FuzzyValue = lower_border
        self.upper_border: FuzzyValue = upper_border

    def __contains__(self, item: FuzzyValue) -> bool:
        return self.lower_border <= item <= self.upper_border

    def __mul__(self, other: FuzzyInterval | None):
        if other is None:
            return None

        if self.lower_border <= other.upper_border and other.lower_border <= self.upper_border:
            return FuzzyInterval(max(self.lower_border, other.lower_border),
                                 min(self.upper_border, other.upper_border))
        else:
            return None

    def __str__(self) -> str:
        if self.lower_border.value == self.upper_border.value:
            return "{" + str(self.lower_border) + "}"
        else:
            return "[" + str(self.lower_border) + ";" + str(self.upper_border) + "]"

    def __eq__(self, other: FuzzyInterval) -> bool:
        return self.lower_border == other.lower_border and self.upper_border == other.upper_border
