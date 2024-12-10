import { open } from 'fs/promises'

async function readInput() {
  const file = await open('src/input/1', 'r')
  const list1: Array<number> = []
  const list2: Array<number> = []

  for await (const line of file.readLines()) {
    const num1 = line.split('   ')[0]
    const num2 = line.split('   ')[1]
    list1.push(parseInt(num1))
    list2.push(parseInt(num2))
  }

  return { list1, list2 }
}

async function solvePart1(list1: Array<number>, list2: Array<number>) {
  list1.sort((a, b) => a - b)
  list2.sort((a, b) => a - b)

  let diff = 0

  for (let i = 0; i < list1.length; i++) {
    diff += (Math.abs(list1[i] - list2[i]))
  }

  console.log(`The total distance between the lists is ${diff}`)
}

async function solvePart2(list1: Array<number>, list2: Array<number>) {
  let similarity = 0

  for (let i = 0; i < list1.length; i++) {
    similarity += list2.reduce((acc, curr) => {
      return (curr === list1[i]) ? acc += list1[i] : acc
    }, 0)
  }

  console.log(`The similarity score of the lists is ${similarity}`)
}

async function solve() {
  console.log('Solving day 1')

  const { list1, list2 } = await readInput()

  await solvePart1(list1, list2)
  await solvePart2(list1, list2)
}

export default solve
