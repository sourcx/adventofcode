const fs = require('fs');

async function getSections(input: string): Promise<Array<number>> {
  const [start, end] = input.split('-').map(Number)
  return Array(end - start + 1).fill(1).map((_, i) => i + start)
}

async function day04a() {
  const lines = fs.readFileSync('in/4', 'utf8').split('\n').filter((line: string) => line.length > 0)

  let nrCompleteOverlap = 0

  for (const line of lines) {
    const one = await getSections(line.split(',')[0])
    const other = await getSections(line.split(',')[1])

    if (one.every((value) => other.includes(value))) {
      nrCompleteOverlap += 1
    } else if (other.every((value) => one.includes(value))) {
      nrCompleteOverlap += 1
    }
  }

  console.log(nrCompleteOverlap)
}

async function day04b() {
  const lines = fs.readFileSync('in/4', 'utf8').split('\n').filter((line: string) => line.length > 0)

  let nrCompleteOverlap = 0

  for (const line of lines) {
    const one = await getSections(line.split(',')[0])
    const other = await getSections(line.split(',')[1])

    if (one.some((value) => other.includes(value))) {
      nrCompleteOverlap += 1
    } else if (other.some((value) => one.includes(value))) {
      nrCompleteOverlap += 1
    }
  }

  console.log(nrCompleteOverlap)
}

export default async function day04() {
  day04a()
  day04b()
}
