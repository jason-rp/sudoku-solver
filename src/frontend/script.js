const solveBtn = document.getElementById("solve");
const error = document.getElementById("error");

solveBtn.addEventListener("click", () => {
  // Get input values and convert to 2D array
  let inputValues = [];
  const inputNodes = document.querySelectorAll('input[type="number"]');
  for (let i = 0; i < inputNodes.length; i++) {
    inputValues.push(Number(inputNodes[i].value));
  }
  const inputMatrix = convertToMatrix(inputValues);

  // Check if input is valid
  if (!isValid(inputMatrix)) {
    error.textContent =
      "Invalid input. Please check your entries and try again.";
    return;
  }

  // Solve sudoku
  const solution = solveSudoku(inputMatrix);

  debugger;
  if (!solution) {
    error.textContent =
      "No solution found. Please check your entries and try again.";
    return;
  }
  else  
  {
    var puzzle = solution.flat().join('');
    $.ajax({
      url: 'https://localhost:7130/api/sudokus',
      type: 'POST',
      data: JSON.stringify({ board: puzzle }),
      contentType: 'application/json',
      success: function(data) {
        console.log('Sudoku solved:', data);
      },
      error: function(xhr, status, error) {
        console.error('Error solving Sudoku:', error);
      }
    });
  }

  for (let i = 0; i < inputNodes.length; i++) {
    inputNodes[i].value = solution[Math.floor(i / 9)][i % 9];
  }

  error.textContent = "";





});

// Utility functions

function convertToMatrix(arr) {
  const matrix = [];
  for (let i = 0; i < 9; i++) {
    matrix.push(arr.slice(i * 9, (i + 1) * 9));
  }
  return matrix;
}

function isValid(matrix) {
  // Check rows
  for (let i = 0; i < 9; i++) {
    const row = matrix[i];
    const used = [];
    for (let j = 0; j < 9; j++) {
      const val = row[j];
      if (val !== 0 && used.includes(val)) {
        return false;
      }
      used.push(val);
    }
  }

  // Check columns
  for (let j = 0; j < 9; j++) {
    const col = matrix.map((row) => row[j]);
    const used = [];
    for (let i = 0; i < 9; i++) {
      const val = col[i];
      if (val !== 0 && used.includes(val)) {
        return false;
      }
      used.push(val);
    }
  }

  // Check regions
  for (let i = 0; i < 9; i += 3) {
    for (let j = 0; j < 9; j += 3) {
      const region = [];
      for (let x = i; x < i + 3; x++) {
        for (let y = j; y < j + 3; y++) {
          region.push(matrix[x][y]);
        }
      }
      const used = [];
      for (let k = 0; k < 9; k++) {
        const val = region[k];
        if (val !== 0 && used.includes(val)) {
          return false;
        }
        used.push(val);
      }
    }
  }

  return true;
}

function solveSudoku(matrix) {
  // Find empty cell
  const emptyCell = findEmptyCell(matrix);
  if (!emptyCell) {
    // All cells filled, return solved matrix
    return matrix;
  }

  const [row, col] = emptyCell;

  // Try numbers 1 to 9 in empty cell
  for (let num = 1; num <= 9; num++) {
    if (isValidGuess(matrix, row, col, num)) {
      // Set guess and continue to next empty cell
      matrix[row][col] = num;
      const solution = solveSudoku(matrix);
      if (solution) {
        // Solved, return solution
        return solution;
      }
      // Backtrack
      matrix[row][col] = 0;
    }
  }

  // No valid guess found, return false
  return false;
}

function findEmptyCell(matrix) {
  for (let i = 0; i < 9; i++) {
    for (let j = 0; j < 9; j++) {
      if (matrix[i][j] === 0) {
        return [i, j];
      }
    }
  }
  return null;
}

function isValidGuess(matrix, row, col, num) {
  // Check row
  for (let j = 0; j < 9; j++) {
    if (matrix[row][j] === num) {
      return false;
    }
  }

  // Check column
  for (let i = 0; i < 9; i++) {
    if (matrix[i][col] === num) {
      return false;
    }
  }

  // Check region
  const regionRow = Math.floor(row / 3) * 3;
  const regionCol = Math.floor(col / 3) * 3;
  for (let i = regionRow; i < regionRow + 3; i++) {
    for (let j = regionCol; j < regionCol + 3; j++) {
      if (matrix[i][j] === num) {
        return false;
      }
    }
  }

  return true;
}
