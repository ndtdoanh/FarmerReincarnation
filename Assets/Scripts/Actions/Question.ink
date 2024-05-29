-> START

=== START ===
Welcome to the quiz game!

-> GET_RANDOM_QUESTION

=== GET_RANDOM_QUESTION ===
~ temp randomIndex = RANDOM(1, 4)
-> QUESTION(randomIndex)

=== QUESTION(randomIndex) ===
{randomIndex:
    - 1: -> QUESTION_1
    - 2: -> QUESTION_2
    - 3: -> QUESTION_3
    - 4: -> QUESTION_4
}

=== QUESTION_1 ===
What is the capital of France?
* Paris -> CORRECT_ANSWER
* London -> WRONG_ANSWER
* Rome -> WRONG_ANSWER
* Berlin -> WRONG_ANSWER
-> ANSWER_CHECK

=== QUESTION_2 ===
Who wrote 'To Kill a Mockingbird'?
* Harper Lee -> CORRECT_ANSWER
* J.K. Rowling -> WRONG_ANSWER
* Mark Twain -> WRONG_ANSWER
* Ernest Hemingway -> WRONG_ANSWER
-> ANSWER_CHECK

=== QUESTION_3 ===
What is the largest planet in our solar system?
* Jupiter -> CORRECT_ANSWER
* Saturn -> WRONG_ANSWER
* Earth -> WRONG_ANSWER
* Mars -> WRONG_ANSWER
-> ANSWER_CHECK

=== QUESTION_4 ===
Who painted the Mona Lisa?
* Leonardo da Vinci -> CORRECT_ANSWER
* Vincent van Gogh -> WRONG_ANSWER
* Pablo Picasso -> WRONG_ANSWER
* Claude Monet -> WRONG_ANSWER
-> ANSWER_CHECK

=== WRONG_ANSWER ===
Sorry, that's not correct.
-> GET_RANDOM_QUESTION

=== ANSWER_CHECK ===
{CORRECT_ANSWER:
-> CORRECT_ANSWER
}

{WRONG_ANSWER:
-> RANDOM_QUESTION
}

=== CORRECT_ANSWER ===
That's correct! Good job.
-> END_GAME

=== RANDOM_QUESTION ===
~ temp randomIndex = RANDOM(1, 4)
-> QUESTION(randomIndex)

=== END_GAME ===
Thank you for playing!

-> DONE