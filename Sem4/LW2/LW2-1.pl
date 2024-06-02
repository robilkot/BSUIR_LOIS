% Предикаты перемещений
move(state(ML, CL, MR, CR, left), state(ML2, CL2, MR2, CR2, right)) :-
    move_action(ML, CL, MR, CR, ML2, CL2, MR2, CR2),
    safe(ML2, CL2, MR2, CR2).

move(state(ML, CL, MR, CR, right), state(ML2, CL2, MR2, CR2, left)) :-
    move_action(MR, CR, ML, CL, MR2, CR2, ML2, CL2),
    safe(ML2, CL2, MR2, CR2).

move_action(XL, YL, XR, YR, XL2, YL2, XR2, YR2) :-
    member([DX, DY], [[2, 0], [0, 2], [1, 1], [1, 0], [0, 1]]),
    XL >= DX, YL >= DY,
    XL2 is XL - DX, YL2 is YL - DY,
    XR2 is XR + DX, YR2 is YR + DY.

% Предикат "безопасного" состояния
safe(ML, CL, MR, CR) :-
    (ML >= CL ; ML = 0),
    (MR >= CR ; MR = 0).

% Вывод результата (списка состояний)
print_solution([]).
print_solution([State|Path]) :-
    print_solution(Path),
    write(State), nl.

% bfs
path(State, State, Path, Path).
path(State, Goal, Visited, Path) :-
    move(State, NextState),
    \+ member(NextState, Visited),
    path(NextState, Goal, [NextState|Visited], Path).

initial_state(state(3, 3, 0, 0, left)).
goal_state(state(0, 0, 3, 3, right)).


% Вызов решения
solve(State, Goal) :-
    path(State, Goal, [State], Path),
    print_solution(Path).

solve(State) :-
    goal_state(Goal),
    solve(State, Goal).

solve :-
    initial_state(State),
    solve(State).
