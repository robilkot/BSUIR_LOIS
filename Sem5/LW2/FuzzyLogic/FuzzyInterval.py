# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление промежутка нечеткого множества
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.


from .FuzzyValue import FuzzyValue
from .functions import invalid_type_error


class FuzzyInterval:
    def __init__(self, lower_border: FuzzyValue, upper_border: FuzzyValue) -> None:
        if upper_border < lower_border:
            upper_border, lower_border = lower_border, upper_border

        if isinstance(lower_border, FuzzyValue):
            self.lower_border: FuzzyValue = lower_border
        elif isinstance(lower_border, float):
            self.lower_border: FuzzyValue = FuzzyValue(lower_border)
        else:
            invalid_type_error(self.__init__, lower_border, FuzzyValue)

        if isinstance(upper_border, FuzzyValue):
            self.upper_border: FuzzyValue = upper_border
        elif isinstance(upper_border, float):
            self.upper_border: FuzzyValue = FuzzyValue(upper_border)
        else:
            invalid_type_error(self.__init__, upper_border, FuzzyValue)

    def __contains__(self, item: FuzzyValue) -> bool:
        if isinstance(item, (FuzzyValue, float)):
            return self.lower_border <= item <= self.upper_border
        else:
            invalid_type_error(self.__contains__, item, FuzzyValue)

    def __mul__(self, other):
        if other is None:
            return None
        if isinstance(other, FuzzyInterval):
            if self.lower_border <= other.upper_border and other.lower_border <= self.upper_border:
                return FuzzyInterval(max(self.lower_border, other.lower_border),
                                     min(self.upper_border, other.upper_border))
            else:
                return None
        else:
            invalid_type_error(self.__mul__, other, FuzzyInterval)

    def __repr__(self) -> str:
        return str(self)

    def __str__(self) -> str:
        if self.lower_border.value == self.upper_border.value:
            return "{" + str(self.lower_border) + "}"
        else:
            return "[" + str(self.lower_border) + ", " + str(self.upper_border) + "]"

    def __eq__(self, other) -> bool:
        if isinstance(other, FuzzyInterval):
            return self.lower_border.value == other.lower_border.value \
                and self.upper_border.value == other.upper_border.value
        else:
            invalid_type_error(self.__eq__, other, FuzzyInterval)
