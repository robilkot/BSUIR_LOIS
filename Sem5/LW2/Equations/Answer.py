# Практическая работа №2 по дисциплине ЛОИС
#  Вариант 3: Запрограммировать обратный нечеткий логический вывод на основе операции нечеткой композиции (max({max({0}U{xi+yi-1})|i})).
#  Выполнили студенты группы 221701 БГУИР:
#  - Робилко Тимур Маркович
#  - Абушкевич Алексей Александрович
#
#  Классы, отвечающие за представление результата решения нечеткого уравнения
#  28.11.2024
#
#  Источники:
# - Нечёткая логика: алгебраические основы и приложения / С.Л. Блюмин, И.А. Шуйкова
# - Логические основы интеллектуальных систем. Практикум / В.В. Голенков и др.

from __future__ import annotations
from Models.Enums import Operations


class Answer(dict):
    def __init__(self, intervals: dict | None, solutions: list | None, type_of_answer: Operations = Operations.AND):
        super().__init__()
        if solutions is None:
            solutions = list()

        self.solutions: list = solutions

        if intervals is None:
            intervals = dict()

        for key, value in intervals.items():
            self[key] = value

        self.type_of_answer: Operations = type_of_answer
        self.have_solution: bool = True

    def add_answer(self, answer: Answer):
        if not self.have_solution:
            return
        if self.type_of_answer == Operations.OR:
            if answer.have_solution:
                self.add_solution(answer)
        elif self.type_of_answer == Operations.AND:
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
        bFinded = False
        for solution1 in self.solutions:
            if solution1 == solution:
                bFinded = True
                break
        if not bFinded:
            self.solutions.append(solution)

    def is_empty(self) -> bool:
        return not self and not self.solutions

    def reduce(self):
        for answer in self.solutions:
            answer.reduce()
        if self.type_of_answer == Operations.AND:
            if not self.solutions:
                return
            for key, value in self.items():
                for answer in self.solutions:
                    if key in answer:
                        answer[key] *= value
                    else:
                        answer[key] = value
                    if answer[key] is None:
                        answer.have_solution = False
            super().clear()
        return

    def __repr__(self):
        result = str()
        for key in sorted(self):
            result += f"{key + ' э ' + str(self[key])}" + '\n'
        for answer in self.solutions:
            if answer.type_of_answer == Operations.AND:
                result += "{" + str(answer) + "}\n"
            elif answer.type_of_answer == Operations.OR:
                result += "[" + str(answer) + "]\n"
        return '\n' + result

    def clear(self) -> None:
        super().clear()
        self.solutions = list()

    def __eq__(self, other):
        if len(self) == len(other) and len(self.solutions) and len(other.solutions):
            for key in self:
                if key not in other:
                    return False
            for solution in self.solutions:
                bFinded = False
                for solution2 in other.solutions:
                    if solution == solution2:
                        bFinded = True
                        break
                if not bFinded:
                    return False
            return True
        return False
