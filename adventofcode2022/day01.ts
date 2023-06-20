const fs = require('fs');
const readline = require('readline');

async function day01a() {
  const fileStream = fs.createReadStream('in/1');

  const rl = readline.createInterface({
    input: fileStream,
    crlfDelay: Infinity
  });

  let largestSum = 0
  let tempSum = 0

  for await (const line of rl) {
    if (line.length > 0) {
      tempSum += parseInt(line)
    } else {
      if (tempSum > largestSum) {
        largestSum = tempSum
      }
      tempSum = 0
    }
  }

  console.log(largestSum)
}

async function day01b() {
  const fileStream = fs.createReadStream('in/1');

  const rl = readline.createInterface({
    input: fileStream,
    crlfDelay: Infinity
  });

  let sums = Array<number>()
  let tempSum = 0

  for await (const line of rl) {
    if (line.length > 0) {
      tempSum += parseInt(line)
    } else {
      sums.push(tempSum)
      tempSum = 0
    }
  }
  sums.push(tempSum)

  sums.sort((n1, n2) => n1 - n2);

  let last1 = sums[sums.length - 1]
  let last2 = sums[sums.length - 2]
  let last3 = sums[sums.length - 3]

  console.log(last1 + last2 + last3)
}

export default async function day01() {
  day01a()
  day01b()
}
