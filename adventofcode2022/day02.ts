const fs = require('fs');

function ShapeScore(input: string): number {
  switch (input) {
    case "X":
      return 1;
    case "Y":
      return 2;
    case "Z":
      return 3;
    default:
      return 0;
  }
}

// Rock: A, X
// Paper: B, Y
// Scissors: C, Z
function RoundScore(opponent: string, me: string): number {
  if (me === "X") {
    if (opponent === "A") {
      return 3
    }
    if (opponent === "B") {
      return 0
    }
    if (opponent === "C") {
      return 6
    }
  }

  if (me === "Y") {
    if (opponent === "A") {
      return 6
    }
    if (opponent === "B") {
      return 3
    }
    if (opponent === "C") {
      return 0
    }
  }

  if (me === "Z") {
    if (opponent === "A") {
      return 0
    }
    if (opponent === "B") {
      return 6
    }
    if (opponent === "C") {
      return 3
    }
  }

  throw new Error('Invalid input: ' + opponent + ' ' + me);
}

async function Day01a() {
  const lines = fs.readFileSync('in/2', 'utf8').split('\n')
  let totalScore = 0

  for await (const line of lines) {
    if (line.length === 0) {
      continue
    }

    let opponent = line[0]
    let me = line[2]

    totalScore += ShapeScore(me)
    totalScore += RoundScore(opponent, me)
  }

  console.log(totalScore)
}

// X means you need to lose
// Y means you need to end the round in a draw
// Z means you need to win
function WhatToPlay(opponent: string, outcome: string): string {
  if (outcome === "X") {
    if (opponent === "A") {
      return "Z"
    }
    if (opponent === "B") {
      return "X"
    }
    if (opponent === "C") {
      return "Y"
    }
  }

  if (outcome === "Y") {
    if (opponent === "A") {
      return "X"
    }
    if (opponent === "B") {
      return "Y"
    }
    if (opponent === "C") {
      return "Z"
    }
  }

  if (outcome === "Z") {
    if (opponent === "A") {
      return "Y"
    }
    if (opponent === "B") {
      return "Z"
    }
    if (opponent === "C") {
      return "X"
    }
  }

  throw new Error('Invalid input: ' + opponent + ' ' + outcome);
}

async function Day01b() {
  const lines = fs.readFileSync('in/2', 'utf8').split('\n')
  let totalScore = 0

  for (const line of lines) {
    if (line.length === 0) {
      continue
    }

    let opponent = line[0]
    let outcome = line[2]
    let me = WhatToPlay(opponent, outcome)

    totalScore += ShapeScore(me)
    totalScore += RoundScore(opponent, me)
  }

  console.log(totalScore)
}

export default async function Day01() {
  Day01a()
  Day01b()
}
