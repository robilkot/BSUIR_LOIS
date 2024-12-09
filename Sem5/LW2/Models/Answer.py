# Практическая работа №2 по дисциплине ЛОИС
# Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
# Выполнили студенты группы 221701 БГУИР:
# - Робилко Тимур Маркович
# - Абушкевич Алексей Александрович
#
# Файл, содержащий реализацию класса вычисленного ответа
# 28.11.2024
#
# Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from __future__ import annotations
from Models.Enums import Operations


class Answer(dict):
    def __init__(self, intervals: dict | None, solutions: list[Answer] | None, type_of_answer: Operations = Operations.AND):
        super().__init__()

        self.solutions: list[Answer] = [] if solutions is None else solutions
        self.type_of_answer: Operations = type_of_answer
        self.have_solution: bool = True

        if intervals is not None:
            for key, value in intervals.items():
                self[key] = value

    def add_answer(self, answer: Answer):
        if not self.have_solution:
            return
        if self.type_of_answer == Operations.OR:
            if answer.have_solution:
                self.add_solution(answer)
        else:
            if answer.is_empty() or not answer.have_solution:
                self.have_solution = False
            elif answer.solutions:
                self.add_solution(answer)
            elif answer:
                self.add_interval(answer)

    def add_interval(self, answer: dict):
        for variable, interval in answer.items():
            if variable in self:
                self[variable] *= interval
            else:
                self[variable] = interval
            if self[variable] is None:
                self.have_solution = False

    def add_solution(self, solution: Answer):
        if solution not in self.solutions:
            self.solutions.append(solution)

    def is_empty(self) -> bool:
        return not self and not self.solutions

    def __str__(self):
        if len(self.solutions) == 0:
            return 'x'.join([str(self[key]) for key in self])
        else:
            answers = [f'({ans})' if ans.type_of_answer == Operations.AND else str(ans) for ans in self.solutions]
            return 'U'.join(answers)

    def clear(self) -> None:
        super().clear()
        self.solutions = list()

    def __eq__(self, other):
        if len(self) != len(other) or not len(self.solutions) or not len(other.solutions):
            return False

        for key in self:
            if key in other:
                return False

        for solution in self.solutions:
            if not other.solutions.__contains__(solution):
                return False

        return True
