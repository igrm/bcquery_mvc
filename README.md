# bcquery-mvc
Online Bitcoin Blockchain Query Tool

Web app that reads blockchain data.

App provides the following output:

Returns a list of blocks since a specified date. Each item in the list contains the block height, block hash, datetime created, transaction count.

Returns a list of transactions of a specified block. Each item in the list contains the transaction hash, confirmation count, a list of inputs, a list of outputs. For each item in the input/output list, the item contains the address, the transaction hash of the input/output and the BTC amount.

Returns a list of transactions of a specified address. Each item in the list contains the transaction hash, confirmation count, a list of inputs, a list of outputs. For each item in the input/output list, the item contains the address, the transaction hash of the input/output and the BTC amount.
