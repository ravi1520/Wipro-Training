const fs=require('fs');
fs.readFile('secondfile.js', 'utf8', (err, data) => {
  if (err) {
    console.error("Error reading file:", err);
    return;
  }
  console.log("Content of secondfile.js:", data);
});