import { readFileSync } from "fs"

async function findMarker(signal: string, count: number) {
  let list = Array<string>()

  for (let i = 0; i < signal.length; i++) {
    list.push(signal[i])

    if (i < count - 1) {
      continue
    }

    let set = new Set(list)

    if (set.size === count) {
      return i + 1
    }

    list.shift()
  }
}

async function day06a() {
  const signal = readFileSync("in/6", "utf8")
  let res = await findMarker(signal, 4)
  console.log(res)
}

async function day06b() {
  const signal = readFileSync("in/6", "utf8")
  let res = await findMarker(signal, 14)
  console.log(res)
}

export default async function day06() {
  day06a()
  day06b()
}
