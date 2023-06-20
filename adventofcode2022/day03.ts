const fs = require('fs');

async function findOverlap2(compartment1: string, compartment2: string): Promise<string> {
  for (const char of compartment1) {
    if (compartment2.includes(char)) return char
  }
  throw new Error()
}

function findOverlap3(bags: string[]): string {
  for (const char of bags[0]) {
    if (bags[1].includes(char) && bags[2].includes(char)) {
      return char
    }
  }
  throw new Error()
}

// a = 97
// A = 65
async function priority(item: string): Promise<number> {
  let utf8Encode = new TextEncoder()
  let char = utf8Encode.encode(item)[0]

  if (char <= 90) {
    return char -= 38
  }

  return char -= 96
}

async function day03a() {
  const lines = fs.readFileSync('in/3', 'utf8').split('\n').filter((line: string) => line.length > 0)

  let prioritySum = 0

  for (const line of lines as string) {
    let compartment1 = line.slice(0, line.length / 2)
    let compartment2 = line.slice(line.length / 2)

    let overlap = await findOverlap2(compartment1, compartment2)
    let prio = await priority(overlap)

    prioritySum += prio
  }

  console.log(prioritySum)
}

async function day03b() {
  const lines = fs.readFileSync('in/3', 'utf8').split('\n').filter((line: string) => line.length > 0)

  let groupLines = Array<string>()
  let prioritySum = 0

  for (const line of lines as string) {
    groupLines.push(line)

    if (groupLines.length === 3) {
      prioritySum += await priority(findOverlap3(groupLines))
      groupLines = []
    }
  }

  console.log(prioritySum)
}

export default async function day03() {
  day03a()
  day03b()
}
