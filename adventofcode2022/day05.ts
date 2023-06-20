import { readFileSync } from "fs";

function parseStacks(stackLines: Array<string>, nrStacks: number): Array<Array<string>> {
  let stacks = Array<Array<string>>()

  for (let i = 0; i < nrStacks; i++) {
    let stack = Array<string>()
    for (let line of stackLines) {
      let crateMark = line.charAt(i * 4 + 1)
      if (crateMark !== ' ' && crateMark !== '') {
        stack.push(crateMark)
      }
    }

    stacks.push(stack)
  }

  return stacks
}

function moveCrates(stacks: Array<Array<string>>, instructions: Array<string>, method: number): Array<Array<string>> {
  for (const instruction of instructions) {
    const [_x, nr, _y, from, _z, to] = instruction.split(' ').map(Number)

    if (method === 9000) {
      for (let i = 0; i < nr; i++) {
        stacks[to - 1].push(stacks[from - 1].pop()!)
      }
    }

    if (method === 9001) {
      let substack = stacks[from - 1].splice(stacks[from - 1].length - nr, nr)
      for (let i = 0; i < nr; i++) {
        stacks[to - 1].push(substack[i])
      }
    }
  }

  return stacks
}

async function rearrange(method: number) {
  const lines = readFileSync("in/5", "utf8").split('\n').filter((line: string) => line.length > 0)

  let stackLines = Array<string>()
  let instructions = Array<string>()
  let nrStacks = 0

  for (const line of lines) {
    if (line.indexOf('[') !== -1) {
      stackLines.push(line)
    } else if (line.charAt(0) === 'm') {
      instructions.push(line)
    } else if (line.indexOf('1') !== -1) {
      const lastNr: string | undefined = line.split(' ').pop()
      if (lastNr === undefined) throw new Error()
      nrStacks = parseInt(lastNr)
    }
  }

  let stacks = parseStacks(stackLines.reverse(), nrStacks)

  stacks = moveCrates(stacks, instructions, method)
  const tops = stacks.map((stack) => stack.pop()).join('')
  console.log(tops)
}

async function day05a() {
  await rearrange(9000)
}

async function day05b() {
  await rearrange(9001)
}

export default async function day05() {
  day05a()
  day05b()
}
