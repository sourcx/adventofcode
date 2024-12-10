import { open } from 'fs/promises'

async function readInput() {
  const file = await open('src/input/2', 'r')
  const reports: Array<Array<number>> = []

  for await (const line of file.readLines()) {
    const report = line.split(' ').map((num) => parseInt(num))
    reports.push(report)
  }

  return reports
}

function isSafe(report: Array<number>): boolean {
  if (report.length <= 1) {
    return true
  }

  const isAscending = (report[0] < report[1])

  for (let i = 1; i < report.length; i++) {
    if (isAscending) {
      if (report[i - 1] >= report[i]) {
        return false
      }
    } else {
      if (report[i - 1] <= report[i]) {
        return false
      }
    }

    if (Math.abs(report[i] - report[i - 1]) > 3) {
      return false
    }
  }

  return true
}


function isSafeWithProblemDampener(report: Array<number>): boolean {
  if (report.length <= 1) {
    return true
  }

  for (let i = 0; i < report.length; i++) {
    const reportCopy = [...report]
    reportCopy.splice(i, 1)
    if (isSafe(reportCopy)) {
      return true
    }
  }

  return false
}

async function solvePart1(reports: Array<Array<number>>) {
  let nrSafeReports = 0

  for (let i = 0; i < reports.length; i++) {
    if (isSafe(reports[i])) {
      nrSafeReports++
    }
  }

  console.log(`The amount or reports that are safe is ${nrSafeReports}`)
}

async function solvePart2(reports: Array<Array<number>>) {
  let nrSafeReports = 0

  for (let i = 0; i < reports.length; i++) {
    if (isSafeWithProblemDampener(reports[i])) {
      nrSafeReports++
    }
  }

  console.log(`The amount or reports that are safe using the Problem Dampener is ${nrSafeReports}`)
}

async function solve() {
  console.log('Solving day 1')

  const reports = await readInput()

  await solvePart1(reports)
  await solvePart2(reports)
}

export default solve
